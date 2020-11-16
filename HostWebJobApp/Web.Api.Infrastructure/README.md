# Web.Api.Infrastructure

## Db Migration

Reference: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/

- Data/Entities 以下に Class かフィールドを追加する

- Command Prompt を起動する (Cloneしたパスに移動)

※Package-Managerでも可能だが、既定のプロジェクトを選択しないといけないし、
パスの指定をしたいのでCommand Promptで実施

```
cd ./WebJobCleanArchitecture\ConsoleAppWebJob\Web.Api.Infrastructure
```

- 環境変数設定

```
set ASPNETCORE_ENVIRONMENT=Development
echo %ASPNETCORE_ENVIRONMENT%
```

- Migrationの作成

```
dotnet ef migrations add IntializeDb
```

- Update Db

```
dotnet ef database update
```

- Columnの追加

```
dotnet ef migrations add Add_[column name]
dotnet ef database update
```


- Tableの追加

```
dotnet ef migrations add Add_[table name]
dotnet ef database update
```