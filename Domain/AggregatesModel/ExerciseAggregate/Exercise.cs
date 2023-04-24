using Domain.SeedWork;

namespace Domain.AggregatesModel.ExerciseAggregate;

public class Exercise : Entity, IAggregateRoot
{
    private readonly List<WorkedMuscle> _workedMuscles;

    public Guid ExerciseId { get; private set; }
    public string ExerciseName { get; private set; } = default!;
    public string? Instructions { get; private set; }
    public UtilityType? UtilityType { get; private set; }
    public MechanicType MechanicType { get; private set; } = default!;
    public MovementType MovementType { get; private set; } = default!;
    public IReadOnlyCollection<WorkedMuscle> WorkedMuscles => _workedMuscles.AsReadOnly();
    public string? Comments { get; private set; }

    protected Exercise()
    {
        ExerciseId = Guid.NewGuid();
        _workedMuscles = new List<WorkedMuscle>();
    }

    protected Exercise(string name, MechanicType mechanicType, MovementType movementType,
                       string? instructions, UtilityType? utilityType, string? comments) : this()
    {
        ExerciseName = name;
        MechanicType = mechanicType;
        MovementType = movementType;
        Instructions = instructions;
        UtilityType = utilityType;
        Comments = comments;

        // TODO: add the ExerciseStarterDomainEvent to the domain events collection.
        // To be raised/dispatched when comitting changes into the Database - After DbContext.SaveChanges()
    }

    public void AddMuscleWorked(Guid id, string name)
    {
        var existingMuscles = _workedMuscles.Where(m => m.MuscleId == id);
        if (existingMuscles.Any())
            throw new InvalidOperationException($"Muscle with Id '{id}' is already present in the list.");

        WorkedMuscle muscleToAdd = new WorkedMuscle(id, name);
        _workedMuscles.Add(muscleToAdd);

        // if it's the only muscle in the list, it becomes the target muscle
        if (_workedMuscles.Count == 1)
            SetTargetMuscleWorked(id);
    }

    public void RemoveMuscleWorked(Guid id)
    {
        var muscleToRemove = _workedMuscles.FirstOrDefault(m => m.MuscleId == id);
        if (muscleToRemove == null)
            throw new ArgumentException($"Muscle with Id '{id}' not found in the list.");

        _workedMuscles.Remove(muscleToRemove);

        // if the muscle removed was the target muscle and there are other muscles remaining in the list
        // put the first muscle of the list as the new target muscle
        if (muscleToRemove.IsTargetMuscle && _workedMuscles.Count > 0)
            _workedMuscles[0].UpdateIsTargetMuscle(true);
    }

    public void SetTargetMuscleWorked(Guid id)
    {
        var newTargetMuscle = _workedMuscles.FirstOrDefault(m => m.MuscleId == id);
        if (newTargetMuscle == null)
            throw new ArgumentException($"Muscle with Id '{id}' not found in the list.");

        // there should be only one target muscle, but just to be sure...
        var currentTargetMuscles = _workedMuscles.Where(m => m.IsTargetMuscle);
        foreach (WorkedMuscle muscle in currentTargetMuscles)
        {
            muscle.UpdateIsTargetMuscle(false);
        }

        newTargetMuscle.UpdateIsTargetMuscle(true);
    }

    public static class Factory
    {
        public static Exercise Create(string name, MechanicType mechanicType, MovementType movementType,
                       string? instructions, UtilityType? utilityType, string? comments)
        {
            //var requested = new ExerciseCreatedEvent(name, mechanicType, movementType, instructions, utilityType, comments);

            Exercise request = new Exercise();
            //request.AddDomainEvent(requested);

            return request;
        }
    }
}
