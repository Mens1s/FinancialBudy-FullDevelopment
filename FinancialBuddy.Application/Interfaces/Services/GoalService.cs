using AutoMapper;
using FinancialBuddy.Application.DTOs.Goal;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Domain.Entities;


namespace FinancialBuddy.Application.Interfaces.Services
{
    public class GoalService : IGoalService
    {
        private readonly IGenericRepository<Goal> _goalRepository;
        private readonly IMapper _mapper;

        public GoalService(IGenericRepository<Goal> goalRepository, IMapper mapper)
        {
            _goalRepository = goalRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GoalDto>> GetAllGoalsAsync(Guid userId)
        {
            var goals = await _goalRepository.FindAsync(g => g.UserId == userId);
            return _mapper.Map<IEnumerable<GoalDto>>(goals);
        }

        public async Task<GoalDto> GetGoalByIdAsync(Guid id)
        {
            var goal = await _goalRepository.GetByIdAsync(id);
            return _mapper.Map<GoalDto>(goal);
        }

        public async Task<GoalDto> CreateGoalAsync(CreateGoalRequest request)
        {
            var goal = _mapper.Map<Goal>(request);
            await _goalRepository.AddAsync(goal);
            await _goalRepository.SaveChangesAsync();
            return _mapper.Map<GoalDto>(goal);
        }

        public async Task UpdateGoalAsync(UpdateGoalRequest request)
        {
            var goal = await _goalRepository.GetByIdAsync(request.Id);
            if (goal != null)
            {
                goal.Name = request.Name;
                goal.TargetAmount = request.TargetAmount;
                goal.SavedAmount = request.SavedAmount;
                goal.TargetDate = request.TargetDate;

                _goalRepository.Update(goal);
                await _goalRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteGoalAsync(Guid id)
        {
            var goal = await _goalRepository.GetByIdAsync(id);
            if (goal != null)
            {
                _goalRepository.Remove(goal);
                await _goalRepository.SaveChangesAsync();
            }
        }
    }
}
