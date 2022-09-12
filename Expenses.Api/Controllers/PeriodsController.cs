using Expenses.Core.Dtos;
using Expenses.Core.Interfaces.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class PeriodsController : ControllerBase
{
    private readonly ILogger<PeriodsController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public PeriodsController(
        ILogger<PeriodsController> logger
        , IUnitOfWork unitOfWork
    )
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IEnumerable<PeriodDto>> Get()
    {
        List<PeriodDto> list;

        list = await _unitOfWork.Period.GetAsync();

        return list;
    }

    [HttpGet("{id}")]
    public async Task<PeriodDto> Get(string id)
    {
        PeriodDto item;

        item = await _unitOfWork.Period.GetAsync(id);

        return item;
    }

    [HttpPost]
    public async Task<IActionResult> Post(PeriodDtoIn period)
    {
        string id;

        id = await _unitOfWork.Period.AddAsync(period);

        return Created("/", new { Id = id });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Put(PeriodDto period, string id)
    {

        await _unitOfWork.Period.UpdateAsync(id, period);

        return Created("/", new { Id = id });
    }

    [HttpGet("/Api/Periods/{periodId}/Entries")]
    public async Task<IEnumerable<EntryDto>> GetEntries(string periodId)
    {
        List<EntryDto> list;

        list = await _unitOfWork.Entry.GetAllAsync(periodId);

        return list;
    }

    [HttpGet("/Api/Periods/{periodId}/Expenses")]
    public async Task<IEnumerable<ExpenseDto>> GetExpenses(string periodId)
    {
        List<ExpenseDto> list;

        list = await _unitOfWork.Expense.GetAllAsync(periodId);

        return list;
    }

}//end class