using System.Diagnostics;

namespace Demo3;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}



















public class BetterWorker : BackgroundService
{
    private readonly ILogger<BetterWorker> _logger;

    public BetterWorker(ILogger<BetterWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Timer t;
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            _logger.LogInformation("Better Worker running at: {time}", DateTimeOffset.Now);
        }
    }
}







































public class LongRunningTask : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var client = new HttpClient();
        try
        {
            var response = await client
                .GetAsync("https://localhost:5001/?seconds=10")
                // .Wait(timeout: TimeSpan.FromSeconds(2)) --> not bad but synchronous
                .WaitAsync(TimeSpan.FromSeconds(5));
        }
        catch (TimeoutException)
        {
            // took too long
        }


        // cacnellation !!!
    }
}





























public class ParallelTask : BackgroundService
{
    private readonly HttpClient client = new();
    private readonly ILogger<ParallelTask> _log;

    public ParallelTask(ILogger<ParallelTask> log)
    {
        _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Parallel
        var jobs = Enumerable.Repeat(10, 50).ToList();
        var options = new ParallelOptions
        {
            // my PC only has 12 threads :-( 
            MaxDegreeOfParallelism = 50
        };

        // ETA
        // (50 * 10) / 12 = 41.6666667

        var watch = new Stopwatch();
        watch.Start();
        //Parallel.ForEach(jobs, options, duration =>
        //{
        //    Job(duration).Wait();
        //});

        _log.LogWarning($"ForEach: It took: {watch.Elapsed}");
        await Task.Delay(TimeSpan.FromSeconds(3));
        watch.Restart();

        await Parallel.ForEachAsync(jobs,
            options,
            async (duration,ct) =>
        {
            await Job(duration);
        });
        _log.LogWarning($"ForEachAsync: It took: {watch.Elapsed}");
    }

    private async Task<string> Job(int seconds)
    {
        var response = await client.GetAsync($"https://localhost:5001/?seconds={seconds}");
        var data = await response.Content.ReadAsStringAsync();
        return data;
    }
}