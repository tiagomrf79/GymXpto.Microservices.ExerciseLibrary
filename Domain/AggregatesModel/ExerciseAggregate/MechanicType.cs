using Domain.SeedWork;

namespace Domain.AggregatesModel.ExerciseAggregate;

public class MechanicType : Enumeration
{
    public static MechanicType Compound = new MechanicType(1, nameof(Compound).ToLowerInvariant());
    public static MechanicType Isolated = new MechanicType(2, nameof(Isolated).ToLowerInvariant());

    public MechanicType(int id, string name) : base(id, name)
    {
    }
}
