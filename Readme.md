# CodeBundler (.NET)

Утилита собирает исходники проекта в один `bundle.txt` (удобно для передачи в AI/LLM).
Сканирует указанную папку проекта, применяет фильтры (исключаемые папки/файлы/расширения) и сохраняет результат **в текущую папку запуска**.

## Требования
- .NET SDK 9.0 (или совместимый)

## Запуск

### 1) Собрать и запустить в режиме разработки
```powershell
dotnet run -- "D:\Path\To\Project"

## Настройка фильтров

### Исключаемые папки (`excludeDirs`):

```csharp
var excludeDirs = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    "bin", "obj", ".git", ".vs", "node_modules"
};

### Исключаемые файлы по имени (excludeFileNames):

```csharp
var excludeFileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".gitignore"
};

### Исключаемые расширения (excludeFileExtensions):

```csharp
var excludeFileExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".code-workspace"
};

### Включаемые расширения (что попадёт в bundle.txt, includeExtensions):

```csharp
var includeExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".cs", ".csproj", ".sln", ".json", ".yml", ".yaml", ".xml", ".md"
};




