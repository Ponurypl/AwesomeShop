using OnboardingIntegrationExample.AwesomeShop.Application.Common;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Abstractions;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Services;

internal sealed class OrderNumberGenService : IOrderNumberGenService
{
    private static readonly SemaphoreSlim Semaphore;
    private static int? _counter;

    private readonly IOrdersRepository _ordersRepository;
    private readonly IDateTime _dateTime;
    
    static OrderNumberGenService()
    {
        Semaphore = new SemaphoreSlim(1);
    }

    public OrderNumberGenService(IOrdersRepository ordersRepository, IDateTime dateTime)
    {
        _ordersRepository = ordersRepository;
        _dateTime = dateTime;
    }

    public async Task<string> GenerateOrderNumberAsync(CancellationToken cancellationToken = default)
    {
        var date = _dateTime.UtcNow;

        await Semaphore.WaitAsync(cancellationToken);

        try
        {
            if (_counter is null)
            {
                _counter = await _ordersRepository.GetNumberOfOrdersFromMonth(date.Month, date.Year, cancellationToken);
            }

            _counter++;
            return $"{_counter}/{date.Month:D2}/{date.Year}";
        }
        finally
        {
            Semaphore.Release();
        }
    }

}