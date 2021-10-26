using Demo3;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
        //services.AddHostedService<BetterWorker>();
        //services.AddHostedService<LongRunningTask>();
        services.AddHostedService<ParallelTask>();
    })
    .Build();

await host.RunAsync();
