using Domain.AggregatesModel.ExerciseAggregate;

namespace Domain;

internal class Class1
{
    public Class1()
    {
        Exercise exercise = new Exercise("Cable Bar Should Press", MechanicType.Compound, MovementType.Push,
            "instructions...", UtilityType.Basic, "comments...");

        exercise.AddMuscleWorked(Guid.NewGuid(), "Deltoid, Anterior");
        exercise.AddMuscleWorked(Guid.NewGuid(), "Deltoid, Lateral");
        exercise.AddMuscleWorked(Guid.NewGuid(), "Supraspinatus");
        exercise.AddMuscleWorked(Guid.NewGuid(), "Triceps Brachii");
        exercise.AddMuscleWorked(Guid.NewGuid(), "Trapezius, Middle");
        exercise.AddMuscleWorked(Guid.NewGuid(), "Trapezius, Lower");
        exercise.AddMuscleWorked(Guid.NewGuid(), "Serratus Anterior, Inferior Digitations");

        foreach (var muscle in exercise.WorkedMuscles)
        {
            Console.WriteLine(muscle.MuscleName);
        }
    }
}
