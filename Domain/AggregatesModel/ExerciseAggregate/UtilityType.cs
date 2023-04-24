using Domain.SeedWork;

namespace Domain.AggregatesModel.ExerciseAggregate;

public class UtilityType : Enumeration
{
    public static UtilityType Basic = new UtilityType(1, nameof(Basic).ToLowerInvariant());
    public static UtilityType Auxiliary = new UtilityType(2, nameof(Auxiliary).ToLowerInvariant());

    public UtilityType(int id, string name) : base(id, name)
    {
    }
}
