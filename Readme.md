# CodeBundler (.NET)

Утилита собирает исходники проекта в один `bundle.txt`
Сканирует указанную папку проекта, применяет фильтры (исключаемые папки/файлы/расширения) и сохраняет результат **в текущую папку запуска**.

## Требования

- .NET SDK 9.0 (или совместимый)

## Запуск

### 1) Собрать и запустить в режиме разработки

```powershell
dotnet run -- "D:\Path\To\Project"
```

## Настройка фильтров

### Исключаемые папки (`EXCLUDE_DIRS`)

```csharp
string[] EXCLUDE_DIRS = [];
```

### Исключаемые файлы по имени (EXCLUDE_FILE_NAMES)

```csharp
string[] EXCLUDE_FILE_NAMES = [];
```

### Исключаемые расширения (EXCLUDE_FILE_EXTENSIONS)

Расширения указываются вместе с точкой (например, .json).

```csharp
string[] EXCLUDE_FILE_EXTENSIONS = [];
```

## Формат bundle.txt

В начале файла записывается служебная информация, затем каждый файл идёт блоком:

```text
FILE: relative\path\to\file.txt
<file content>
```
