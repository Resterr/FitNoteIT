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
			new(Guid.NewGuid(), "Wyciskanie sztangi",
				"Ćwiczenie polega na opuszczaniu i unoszeniu sztangi na ławce poziomej, angażując mięśnie klatki piersiowej, tricepsa i barków.", categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Pompki", "Pompki są prostym ćwiczeniem, w którym leżysz na podłodze i unosisz ciało, napinając mięśnie klatki piersiowej i ramion.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Rozpiętki", "Rozpiętki polegają na rozsuwaniu ramion z hantlami w rękach lub na maszynie, angażując mięśnie klatki piersiowej.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Wyciskanie hantli", "Ćwiczenie to polega na wyciskaniu hantli na ławce skośnej, co aktywuje różne partie mięśni klatki piersiowej i ramion.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Wyciskanie na maszynie",
				"Osoba wykonująca ćwiczenie przesuwa uchwyty na maszynie, aby wypchnąć ciężar w górę i w kierunku klatki piersiowej, angażując mięśnie tej części ciała.",
				categories.Single(x => x.Name == "Klatka piersiowa")),
			new(Guid.NewGuid(), "Martwy ciąg", "Martwy ciąg to ćwiczenie polegające na unoszeniu ciężaru z ziemi. Angażuje głównie mięśnie pleców, dolnej części pleców, pośladków i nóg.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Wiosłowanie sztangą",
				"Wiosłowanie sztangą to ćwiczenie, w którym schylasz się i unosz sztangę, napinając mięśnie pleców. To doskonałe ćwiczenie na rozwinięcie mięśni grzbietu.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Podciąganie na drążku",
				"Podciąganie na drążku szerokim chwytem angażuje mięśnie pleców, ramion i mięśnie naramienne. Pomaga w rozwoju siły i szerokiej części pleców.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Wiosłowanie hantlami",
				"Wiosłowanie hantlami w opadzie tułowia to ćwiczenie, które skupia się na mięśniach środkowej części pleców. Unosząc hantle, wzmacniasz plecy i mięśnie obręczy barkowej.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Face pulls", "Face pulls to ćwiczenie, które pomaga w rozwoju mięśni trapezów i obręczy barkowej. Wykonuje się je przy użyciu liny i drążka wyciągu górnego.",
				categories.Single(x => x.Name == "Plecy")),
			new(Guid.NewGuid(), "Plank",
				"Plank to ćwiczenie, w którym utrzymujesz pozycję deski, opierając się na przedramionach i palcach u stóp. To doskonałe ćwiczenie na wzmocnienie mięśni brzucha i korzenia kręgosłupa.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Wznosy nóg",
				"Wznosy nóg to ćwiczenie, w którym unosisz nogi do góry, wisząć pionowo na drążku.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Side plank",
				"Side plank to ćwiczenie, w którym utrzymujesz pozycję deski, opierając się na przedramionach i palcach u stóp. To doskonałe ćwiczenie na wzmocnienie mięśni brzucha i korzenia kręgosłupa.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Mountain climbers",
				"Mountain climbers to dynamiczne ćwiczenie, w którym unosisz naprzemiennie kolana ku klatce piersiowej. To doskonały sposób na pracę nad mięśniami brzucha i poprawę wytrzymałości.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Russian twists",
				"Russian twists to ćwiczenie, w którym siedzisz na podłodze i obracasz tułów w lewo i prawo, utrzymując równowagę. To świetne ćwiczenie dla mięśni skośnych brzucha.",
				categories.Single(x => x.Name == "Brzuch")),
			new(Guid.NewGuid(), "Wyciskanie francuskie",
				"Wyciskanie francuskie to ćwiczenie, w którym unosisz sztangę nad głowę i zginałeś ramiona w łokciach, skupiając się na mięśniach tricepsa.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Pompki diamentowe", "Pompki diamentowe to modyfikacja tradycyjnych pompek, w której ręce są zbliżone, co intensyfikuje pracę mięśni tricepsa.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Ściąganie na linkach",
				"Ściąganie to ćwiczenie, w którym ściągasz linkę w dół, napinając mięśnie tricepsa. To świetne ćwiczenie izolujące.", categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Dipy",
				"Dipy to ćwiczenie, w którym opierasz się na poręczach i unosz ciało, angażując mięśnie tricepsa. To doskonałe ćwiczenie dla tricepsa.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Linki za głowę",
				"Linki za głowę to ćwiczenie, w którym ściągasz linkę zza głowy.",
				categories.Single(x => x.Name == "Triceps")),
			new(Guid.NewGuid(), "Uginanie ramion ze hantlami", "Uginanie ramion ze hantlami to ćwiczenie, w którym unosisz sztangielki ku górze, skupiając się na mięśniach bicepsa.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Uginanie ramion ze sztangą łamaną",
				"Uginanie ramion ze sztangą łamaną to ćwiczenie, w którym unosisz sztangę łamaną ku górze, napinając mięśnie bicepsa. Sztanga łamana redukuje nacisk na nadgarstki.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Uginanie ramion na maszynie",
				"Uginanie ramion na maszynie to ćwiczenie, które izoluje mięśnie bicepsa. Wykonuje się je na specjalnej maszynie, co pomaga w precyzyjnym treningu bicepsa.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Uginanie ramion na modlitewniku",
				"Uginanie ramion na modlitewniku to ćwiczenie, w którym unosisz hantle ku górze, trzymając je w pozycji neutralnej (chwytem młotkowym). To ćwiczenie angażuje mięśnie bicepsa oraz mięśnie przedramienia.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Herkulesy",
				"Herkulesy to ćwiczenia na bramie, polegające na ściągnięcie linek uginając ramiona.",
				categories.Single(x => x.Name == "Biceps")),
			new(Guid.NewGuid(), "Przysiady",
				"Przysiady to podstawowe ćwiczenie na mięśnie nóg. Stojąc z ciężarem na plecach lub przed sobą, schylasz się, zginając kolana i unosząc się z powrotem. Angażuje mięśnie ud, pośladków i mięśnie rdzenia.",
				categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Wypychanie nóg na maszynie",
				"Wypychanie nóg na maszynie to ćwiczenie, w którym przepychasz ciężar nogami. To doskonałe ćwiczenie na rozwinięcie mięśni ud i pośladków.", categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Wykroki",
				"Wykroki to ćwiczenie wykonywane w celu wzmocnienia mięśni nóg, zwłaszcza mięśni ud i pośladków.",
				categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Wspięcia na palce",
				"Wspięcia na palce to ćwiczenie skupiające się na mięśniach łydek. Stojąc na palcach, unosisz pięty ku górze, napinając mięśnie łydek.", categories.Single(x => x.Name == "Nogi")),
			new(Guid.NewGuid(), "Martwy ciąg rumuński",
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