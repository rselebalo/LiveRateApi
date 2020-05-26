using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class RatesService : IRatesService
{
    private readonly RatesDbContext _ratesDbContext;
    public RatesService(RatesDbContext ratesDbContext)
    {
        _ratesDbContext = ratesDbContext;
    }
    public async Task<bool> SaveRatesAsync(List<CurrencyPairModel> inputModelList)
    {
        try
        {
            foreach (var inputModel in inputModelList)
            {
                // // check if exists
                var exists = await _ratesDbContext.Rates.AnyAsync(r => r.currency == inputModel.currency && r.rate == inputModel.rate && r.timestamp == inputModel.timestamp);

                if (!exists)
                {
                    // Map input model to rates db model
                    RatesDataModel rate = new RatesDataModel();
                    rate.currency = inputModel.currency;
                    rate.rate = inputModel.rate;
                    rate.bid = inputModel.bid;
                    rate.ask = inputModel.ask;
                    rate.high = inputModel.high;
                    rate.low = inputModel.low;
                    rate.open = inputModel.open;
                    rate.close = inputModel.close;
                    rate.timestamp = inputModel.timestamp;

                    // add rates data to the db
                    _ratesDbContext.Rates.Add(rate);
                }
            }
            // save changes and return true
            return await _ratesDbContext.SaveChangesAsync() > 0;
        }
        catch
        {
            return false;
        }
    }
}