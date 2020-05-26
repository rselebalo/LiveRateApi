using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRatesService
{
    Task<bool> SaveRatesAsync(List<CurrencyPairModel> inputModelList);
}