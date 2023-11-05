using FitNoteIT.Modules.Exercises.Core.Abstractions;
using FitNoteIT.Modules.Exercises.Core.Entities;

namespace FitNoteIT.Modules.Exercises.Core.Persistence.Seeders;

internal interface IExercisesSeeder
{
	void SeedCategories();
	void SeedExercises();
}

internal sealed class ExercisesSeeder : IExercisesSeeder
{
	private readonly IExercisesDbContext _dbContext;

	public ExercisesSeeder(IExercisesDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void SeedCategories()
	{
		var categories = new List<Category>
		{
			new(Guid.NewGuid(), "Klatka piersiowa"),
			new(Guid.NewGuid(), "Plecy"),
			new(Guid.NewGuid(), "Brzuch"),
			new(Guid.NewGuid(), "Triceps"),
			new(Guid.NewGuid(), "Biceps"),
			new(Guid.NewGuid(), "Nogi")
		};

		var currentCategories = _dbContext.Categories.ToList();
		var categoriesToAdd = categories.Where(item1 => !currentCategories.Any(item2 => item2.Name == item1.Name))
			.ToList();

		_dbContext.Categories.AddRange(categoriesToAdd);
		_dbContext.SaveChanges();
	}

	public void SeedExercises()
	{
		var categories = _dbContext.Categories.ToList();

		var exercises = new List<Exercise>
		{
			new(Guid.NewGuid(), "Wypychanie sztangi na ławce poziomej",
				"Ćwiczenie polega na opuszczaniu i unoszeniu sztangi na ławce poziomej, angażując mięśnie klatki piersiowej, tricepsa i barków.", categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Pompki", "Pompki są prostym ćwiczeniem, w którym leżysz na podłodze i unosisz ciało, napinając mięśnie klatki piersiowej i ramion.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Rozpiętki na maszynie lub hantlach", "Rozpiętki polegają na rozsuwaniu ramion z hantlami w rękach lub na maszynie, angażując mięśnie klatki piersiowej.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Wyciskanie hantli na ławce skośnej", "Ćwiczenie to polega na wyciskaniu hantli na ławce skośnej, co aktywuje różne partie mięśni klatki piersiowej i ramion.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Pompki diamentowe",
				"Pompki diamentowe są modyfikacją tradycyjnych pompek, w których ręce są bliżej siebie, co stawia większy nacisk na mięśnie klatki piersiowej i tricepsa.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Martwy ciąg", "Martwy ciąg to ćwiczenie polegające na unoszeniu ciężaru z ziemi. Angażuje głównie mięśnie pleców, dolnej części pleców, pośladków i nóg.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Wiosłowanie sztangą",
				"Wiosłowanie sztangą to ćwiczenie, w którym schylasz się i unosz sztangę, napinając mięśnie pleców. To doskonałe ćwiczenie na rozwinięcie mięśni grzbietu.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Podciąganie na drążku szerokim chwytem",
				"Podciąganie na drążku szerokim chwytem angażuje mięśnie pleców, ramion i mięśnie naramienne. Pomaga w rozwoju siły i szerokiej części pleców.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Wiosłowanie hantlami w opadzie tułowia",
				"Wiosłowanie hantlami w opadzie tułowia to ćwiczenie, które skupia się na mięśniach środkowej części pleców. Unosząc hantle, wzmacniasz plecy i mięśnie obręczy barkowej.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Face pulls", "Face pulls to ćwiczenie, które pomaga w rozwoju mięśni trapezów i obręczy barkowej. Wykonuje się je przy użyciu liny i drążka wyciągu górnego.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Plank",
				"Plank to ćwiczenie, w którym utrzymujesz pozycję deski, opierając się na przedramionach i palcach u stóp. To doskonałe ćwiczenie na wzmocnienie mięśni brzucha i korzenia kręgosłupa.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Unoszenie nóg w leżeniu na plecach",
				"Unoszenie nóg w leżeniu na plecach to ćwiczenie, w którym unosisz nogi do góry, napinając mięśnie brzucha. Pomaga w rozwijaniu mięśni dolnego brzucha.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Przysiady z podnoszeniem nóg",
				"Przysiady z podnoszeniem nóg angażują mięśnie brzucha, pleców i nóg. To ćwiczenie wzmacnia zarówno mięśnie rdzenia, jak i dolne partie ciała.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Mountain climbers",
				"Mountain climbers to dynamiczne ćwiczenie, w którym unosisz naprzemiennie kolana ku klatce piersiowej. To doskonały sposób na pracę nad mięśniami brzucha i poprawę wytrzymałości.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Russian twists",
				"Russian twists to ćwiczenie, w którym siedzisz na podłodze i obracasz tułów w lewo i prawo, utrzymując równowagę. To świetne ćwiczenie dla mięśni skośnych brzucha.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Wyciskanie francuskie (French Press)",
				"Wyciskanie francuskie to ćwiczenie, w którym unosisz sztangę nad głowę i zginałeś ramiona w łokciach, skupiając się na mięśniach tricepsa.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Pompki diamentowe", "Pompki diamentowe to modyfikacja tradycyjnych pompek, w której ręce są zbliżone, co intensyfikuje pracę mięśni tricepsa.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Wyciskanie na wyciągu górnym (Tricep Pushdown)",
				"Wyciskanie na wyciągu górnym to ćwiczenie, w którym wyciągasz drążek w dół, napinając mięśnie tricepsa. To świetne ćwiczenie izolujące.", categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Dipy na poręczach",
				"Dipy na poręczach to ćwiczenie, w którym opierasz się na poręczach i unosz ciało, angażując mięśnie tricepsa. To doskonałe ćwiczenie dla tricepsa.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Unoszenie hantli w opadzie tułowia",
				"Unoszenie hantli w opadzie tułowia to ćwiczenie, w którym unosisz hantle ku górze, koncentrując się na mięśniach tricepsa. Pomaga w ich rozwoju.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Uginanie ramion ze sztangielkami", "Uginanie ramion ze sztangielkami to ćwiczenie, w którym unosisz sztangielki ku górze, skupiając się na mięśniach bicepsa.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Uginanie ramion ze sztangą łamaną (E-Z Bar Curl)",
				"Uginanie ramion ze sztangą łamaną to ćwiczenie, w którym unosisz sztangę łamaną ku górze, napinając mięśnie bicepsa. Sztanga łamana redukuje nacisk na nadgarstki.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Uginanie ramion na maszynie do uginania",
				"Uginanie ramion na maszynie do uginania to ćwiczenie, które izoluje mięśnie bicepsa. Wykonuje się je na specjalnej maszynie, co pomaga w precyzyjnym treningu bicepsa.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Uginanie ramion na modlitewniku (Hammer Curl)",
				"Uginanie ramion na modlitewniku to ćwiczenie, w którym unosisz hantle ku górze, trzymając je w pozycji neutralnej (chwytem młotkowym). To ćwiczenie angażuje mięśnie bicepsa oraz mięśnie przedramienia.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Uginanie ramion ze sztangą w nachwycie",
				"Uginanie ramion ze sztangą w nachwycie to ćwiczenie, w którym unosisz sztangę ku górze, trzymając ją w szerokim chwycie. To doskonałe ćwiczenie na rozwinięcie bicepsa.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Przysiady (Squats)",
				"Przysiady to podstawowe ćwiczenie na mięśnie nóg. Stojąc z ciężarem na plecach lub przed sobą, schylasz się, zginając kolana i unosząc się z powrotem. Angażuje mięśnie ud, pośladków i mięśnie rdzenia.",
				categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Wypychanie nóg na maszynie (Leg Press)",
				"Wypychanie nóg na maszynie to ćwiczenie, w którym przepychasz ciężar nogami. To doskonałe ćwiczenie na rozwinięcie mięśni ud i pośladków.", categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Martwy ciąg (Deadlift)",
				"Martwy ciąg to ćwiczenie, które angażuje mięśnie nóg, pleców, pośladków i mięśnie dolnej części pleców. Polega na podnoszeniu ciężaru z podłogi.",
				categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Wspięcia na palce (Calf Raises)",
				"Wspięcia na palce to ćwiczenie skupiające się na mięśniach łydek. Stojąc na palcach, unosisz pięty ku górze, napinając mięśnie łydek.", categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Martwy ciąg rumuński (Romanian Deadlift)",
				"Martwy ciąg rumuński to wersja martwego ciągu, która bardziej koncentruje się na mięśniach nóg i pośladków. Unosząc ciężar, utrzymujesz nogi proste.",
				categories.Single(x => x.Name == "Nogi"))
		};

		var currentExercises = _dbContext.Exercises.ToList();
		var exercisesToAdd = exercises.Where(item1 => !currentExercises.Any(item2 => item2.Name == item1.Name))
			.ToList();

		_dbContext.Exercises.AddRange(exercisesToAdd);
		_dbContext.SaveChanges();
	}
}