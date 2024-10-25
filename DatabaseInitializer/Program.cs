
using DatabaseInitializer;

const string basePath = "D:\\REPOs\\FreeLearn\\Src\\Learning_System\\DatabaseInitializer\\T-SQL";

string connectionString = "Server=.; Integrated Security=True;";
string databaseGeneratorPath = Path.Combine(basePath,"CreateDatabase.sql");
string tablesGeneratorPath = Path.Combine(basePath, "CreateTables.sql");

DatabaseInit initializer = new DatabaseInit(connectionString);

bool DbCreated = await initializer.InitializeDatabaseAsync(databaseGeneratorPath);
Console.WriteLine(DbCreated ? "Database initialized successfully!" : "Fail To Generate Database");

bool TblCreated = await initializer.InitializeDatabaseAsync(tablesGeneratorPath);
Console.WriteLine(TblCreated ? "Tables initialized successfully!" : "Fail To Generate Tables");

              


