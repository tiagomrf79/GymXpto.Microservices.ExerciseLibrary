using Domain.SeedWork;

namespace Domain.AggregatesModel.ExerciseAggregate;

public class MovementType : Enumeration
{
    public static MovementType Push = new MovementType(1, nameof(Push).ToLowerInvariant());
    public static MovementType Pull = new MovementType(2, nameof(Pull).ToLowerInvariant());

    public MovementType(int id, string name) : base(id, name)
    {
    }
}
