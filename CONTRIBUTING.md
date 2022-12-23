## Руководство по разработке

### Начало работы

1. Публицизировать подключаемые в [Directory.Build.props](Directory.Build.props) референсы игры и закинуть их в папку `references`.
2. Прочитать это руководство до конца.

### Версионирование

Мы следуем [календарному версионированию](https://calver.org/), и придерживаемся следующего формата:

```yml
YYYY.MM0D.MICRO
2021.1231.0   # 31 декабря 2021, первый патч за этот день
2020.101.4    # 1 января 2020, пятый патч за этот день
```

### Ветки

1. Основной веткой является `master`.
2. Веткой для разработки является `dev`.
3. В основной ветке находятся стабильные и готовые к использованию на серверах плагины.
4. Каждая разработка ведётся в своей отдельной ветке, но постоянно синхронизируется с веткой `dev`.
5. Названия веток должно быть в [`kebab-case`](https://en.toolpage.org/tool/kebabcase).

### Пайплайн релизов:

```yml
feature ----> dev ----> master ----> production
          1         2            3

1. Функционал реализован и протестирован
2. Плагины протестированы на совместимость и наличие конфликтов
3. Релиз на сервера
```

### Коммиты

Мы следуем [Conventional Commits](https://www.conventionalcommits.org). Формат шапки коммита может быть следующим:

```c
<type>(<scope>): <description>

- <type>: одно из `build|ci|docs|feat|fix|refactor|chore`.
- <scope>: название плагин*, в котором производились изменения (иначе ничего).
- <description>: краткое описание до 80 символов без заглавных и без точек в конце.
```

*название плагина пишется в [`kebab-case`](https://en.toolpage.org/tool/kebabcase).

### Пул-реквесты

Пул-реквест открывается тогда, когда плагин считается готовым к релизу. У нас есть [универсальный шаблон](.github/pull_request/template.md) для каждого проекта с небольшим чек-листом, который обязательно напомнит о чём-то важном.

### Шаблон проекта

Rider/Visual Studio имеют разные шаблоны по умолчанию, и они нам не совсем подходят.

После создания проекта необходим проделать ряд вещей:

1. Удалить папку `Properties`
2. Привести `.csproj` файл к тому виде, что указан в примере:

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup Label="Project">
        <!-- Обрати внимание на GUID, RootNamspace, и AssemblyName -->
        <ProjectGuid>{EDC9684C-FAE4-41AB-9B0A-6E6B8C28B0DA}</ProjectGuid>
        <OutputType>Library</OutputType>
        <RootNamespace>NekoLib</RootNamespace>
        <AssemblyName>NekoLib</AssemblyName>
    </PropertyGroup>

    <ItemGroup>
        <Reference Include="System"/>
    </ItemGroup>
</Project>
```

### Style Guide

В основном используются базовые правила линтинга и авто-форматирование от Rider и ReSharper.

- Всегда используйте `var`.
- Называйте переменные коротко, но понятно.
- Всегда открываем блоки там, где это возможно (к примеру в `if`/`for`/`while`).
- Строго соблюдайте [C# Coding Standards and Naming Conventions](https://github.com/ktaranov/naming-convention/blob/master/C%23%20Coding%20Standards%20and%20Naming%20Conventions.md) (кроме пунктов **9, 18, 27**).
