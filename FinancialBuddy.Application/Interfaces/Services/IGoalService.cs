using FinancialBuddy.Application.DTOs.Goal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialBuddy.Application.Interfaces.Services
{
    public interface IGoalService
    {
        Task<IEnumerable<GoalDto>> GetAllGoalsAsync(Guid userId);
        Task<GoalDto> GetGoalByIdAsync(Guid id);
        Task<GoalDto> CreateGoalAsync(CreateGoalRequest request);
        Task UpdateGoalAsync(UpdateGoalRequest request);
        Task DeleteGoalAsync(Guid id);
    }
}
