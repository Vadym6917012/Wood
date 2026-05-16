using Wood.Domain.Entities;

namespace Wood.Infrastructure.Persistence
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if ( db.Categories.Any() ) return;

            // ✅ БЕЗ явних Id — SQL Server генерує сам
            var cat1 = new Category { Name = "Дошки обрізні", Slug = "obrizni", Icon = "🪵" };
            var cat2 = new Category { Name = "Дошки необрізні", Slug = "neobrizni", Icon = "🌲" };
            var cat3 = new Category { Name = "Брус та балки", Slug = "brus", Icon = "🔩" };
            var cat4 = new Category { Name = "Вагонка", Slug = "vagonka", Icon = "🏠" };
            var cat5 = new Category { Name = "Паркет та підлога", Slug = "parket", Icon = "✨" };

            db.Categories.AddRange(cat1, cat2, cat3, cat4, cat5);
            db.SaveChanges();

            db.Products.AddRange(
                new Product
                {
                    Name = "Дошка обрізна сосна 25×100×6000",
                    Species = "Сосна",
                    Grade = "1 сорт",
                    Dimensions = "25×100×6000 мм",
                    PricePerCubicMeter = 7200,
                    PricePerPiece = 108,
                    Unit = "м³",
                    Category = cat1,
                    InStock = true,
                    IsFeatured = true,
                    Description = "Суха обрізна дошка з сосни першого сорту.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Дошка обрізна сосна 25×150×6000",
                    Species = "Сосна",
                    Grade = "1 сорт",
                    Dimensions = "25×150×6000 мм",
                    PricePerCubicMeter = 7200,
                    PricePerPiece = 162,
                    Unit = "м³",
                    Category = cat1,
                    InStock = true,
                    IsFeatured = true,
                    Description = "Широка обрізна дошка з сосни.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Дошка обрізна дуб 32×120×4000",
                    Species = "Дуб",
                    Grade = "1 сорт",
                    Dimensions = "32×120×4000 мм",
                    PricePerCubicMeter = 18500,
                    PricePerPiece = 284,
                    Unit = "м³",
                    Category = cat1,
                    InStock = true,
                    IsFeatured = true,
                    Description = "Преміальна дубова дошка для меблів та підлог.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Дошка обрізна ялина 25×100×6000",
                    Species = "Ялина",
                    Grade = "2 сорт",
                    Dimensions = "25×100×6000 мм",
                    PricePerCubicMeter = 6800,
                    PricePerPiece = 102,
                    Unit = "м³",
                    Category = cat1,
                    InStock = true,
                    Description = "Економ-варіант із ялини для будівельних робіт.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Дошка необрізна сосна 25мм",
                    Species = "Сосна",
                    Grade = "—",
                    Dimensions = "25 мм, довжина 4-6 м",
                    PricePerCubicMeter = 4500,
                    PricePerPiece = 0,
                    Unit = "м³",
                    Category = cat2,
                    InStock = true,
                    Description = "Сирова необрізна дошка для огорож та обшивки.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Дошка необрізна дуб 40мм",
                    Species = "Дуб",
                    Grade = "—",
                    Dimensions = "40 мм, довжина 2-5 м",
                    PricePerCubicMeter = 9800,
                    PricePerPiece = 0,
                    Unit = "м³",
                    Category = cat2,
                    InStock = false,
                    Description = "Масивна необрізна дубова дошка для столярних виробів.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Брус сосна 100×100×6000",
                    Species = "Сосна",
                    Grade = "1 сорт",
                    Dimensions = "100×100×6000 мм",
                    PricePerCubicMeter = 8200,
                    PricePerPiece = 492,
                    Unit = "м³",
                    Category = cat3,
                    InStock = true,
                    IsFeatured = true,
                    Description = "Будівельний брус із сосни для каркасів та несучих конструкцій.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Брус сосна 150×150×6000",
                    Species = "Сосна",
                    Grade = "1 сорт",
                    Dimensions = "150×150×6000 мм",
                    PricePerCubicMeter = 8200,
                    PricePerPiece = 1107,
                    Unit = "м³",
                    Category = cat3,
                    InStock = true,
                    Description = "Великогабаритний брус для лагів та несучих стін.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Балка дерев'яна 50×200×6000",
                    Species = "Сосна",
                    Grade = "1 сорт",
                    Dimensions = "50×200×6000 мм",
                    PricePerCubicMeter = 8500,
                    PricePerPiece = 510,
                    Unit = "м³",
                    Category = cat3,
                    InStock = true,
                    Description = "Балка для перекриттів та покрівельних систем.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Вагонка сосна 12×96×2000 Євро",
                    Species = "Сосна",
                    Grade = "А",
                    Dimensions = "12×96×2000 мм",
                    PricePerCubicMeter = 0,
                    PricePerPiece = 45,
                    Unit = "пог.м",
                    Category = cat4,
                    InStock = true,
                    IsFeatured = true,
                    Description = "Євровагонка сосна класу А для лазень та саун.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Вагонка липа 12×96×2000",
                    Species = "Липа",
                    Grade = "А",
                    Dimensions = "12×96×2000 мм",
                    PricePerCubicMeter = 0,
                    PricePerPiece = 95,
                    Unit = "пог.м",
                    Category = cat4,
                    InStock = true,
                    Description = "Вагонка з липи — ідеальна для сауни та лазні.",
                    ImageUrl = ""
                },

                new Product
                {
                    Name = "Паркетна дошка дуб 18×120×2200",
                    Species = "Дуб",
                    Grade = "Преміум",
                    Dimensions = "18×120×2200 мм",
                    PricePerCubicMeter = 0,
                    PricePerPiece = 420,
                    Unit = "м²",
                    Category = cat5,
                    InStock = true,
                    IsFeatured = true,
                    Description = "Масивна паркетна дошка з дуба. Покрита олією.",
                    ImageUrl = ""
                }
            );
            db.SaveChanges();
        }
    }
}
