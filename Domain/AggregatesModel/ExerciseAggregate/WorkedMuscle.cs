// TODO: improve error message when saving a muscle name

using Domain.SeedWork;

namespace Domain.AggregatesModel.ExerciseAggregate;

public class WorkedMuscle : Entity
{
    public Guid MuscleId { get; private set; }
    public string MuscleName { get; private set; } = string.Empty;
    public bool IsTargetMuscle { get; private set; } = false;

    internal WorkedMuscle(Guid id, string name)
    {
        MuscleId = id;
        UpdateMuscleName(name);
    }

    internal void UpdateIsTargetMuscle(bool isTargetMuscle)
    {
        IsTargetMuscle = isTargetMuscle;
    }

    internal void UpdateMuscleName(string newMuscleName)
    {
        if (string.IsNullOrEmpty(newMuscleName))
            throw new ArgumentNullException(nameof(newMuscleName));

        MuscleName = newMuscleName;
    }
}
