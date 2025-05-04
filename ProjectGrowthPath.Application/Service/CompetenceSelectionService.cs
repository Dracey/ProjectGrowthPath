using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.Service;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Domain.Entities;

public class CompetenceSelectionService : ICompetenceSelectionService
{
    private readonly ICompetenceRepository _competenceRepository;
    private readonly SetupStateStore _stateStore;
    private readonly IFirstTimeSetupService _setupService;
    private List<Competence> _allCompetences = new();

    public CompetenceSelectionService(ICompetenceRepository competenceRepository, SetupStateStore stateStore, IFirstTimeSetupService setupService)
    {
        _competenceRepository = competenceRepository;
        _stateStore = stateStore;
        _setupService = setupService;

    }

    public async Task InitializeAsync()
    {
        _allCompetences = await _competenceRepository.GetList();
        await _stateStore.LoadAsync();
    }

    public Competence? GetSelectedCompetence(int number, string mode)
    {
        var dict = GetDictionary(mode);
        dict.TryGetValue(number, out var competence);
        return competence;
    }

    public IEnumerable<Competence> GetAvailableCompetences(int currentNumber, string mode)
    {
        var selected = GetDictionary(mode)
            .Where(kvp => kvp.Key != currentNumber)
            .Select(kvp => kvp.Value)
            .Where(c => c != null)
            .ToList();

        var selectedOtherMode = GetDictionary(mode == "skills" ? "interests" : "skills")
            .Select(kvp => kvp.Value)
            .Where(c => c != null)
            .ToList();

        var excluded = selected.Concat(selectedOtherMode).Distinct().ToList();

        return _allCompetences
            .Where(c => !excluded.Any(e => e.CompetenceID == c.CompetenceID))
            .ToList();
    }

    public async Task SelectCompetenceAsync(int number, Competence? competence, string mode)
    {
        await _setupService.UpdateCompetenceDictionary(number, competence, mode);
    }

    public bool CanProceed(string mode)
    {
        var dict = GetDictionary(mode);
        return dict.Count == 3
               && dict.Values.Distinct().Count() == 3
               && dict.Values.All(c => c != null);
    }

    public Task<IEnumerable<Competence>> SearchCompetencesAsync(string value, string mode)
    {
        var available = GetAvailableCompetences(-1, mode);
        if (string.IsNullOrWhiteSpace(value))
            return Task.FromResult(available);

        var result = available
            .Where(c => c.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        return Task.FromResult(result);
    }

    private Dictionary<int, Competence> GetDictionary(string mode)
    {
        return mode == "skills"
            ? _stateStore.CurrentState.SelectedSkills
            : _stateStore.CurrentState.SelectedInterests;
    }
}
