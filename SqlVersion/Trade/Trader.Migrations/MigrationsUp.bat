REM Options = https://fluentmigrator.github.io/articles/runners/runner-console.html
REM Runner = https://fluentmigrator.github.io/articles/migration-runners.html?tabs=vs-pkg-manager-console

cd\
cd _leandro\Trade\Trade\Publicacao\Migrations

REM LIST
"C:\_leandro\Trade\Trade\packages\FluentMigrator.Console.3.1.3\net461\x64\Migrate.exe" --target=Trader.Migrations.dll --db=SqlServer2014 --c="Data Source=LEANDRO\SQLEXPRESS;Initial Catalog=Trader;Integrated Security=True;Pooling=False" --task listmigrations
REM DOWN
"C:\_leandro\Trade\Trade\packages\FluentMigrator.Console.3.1.3\net461\x64\Migrate.exe" --target=Trader.Migrations.dll --db=SqlServer2014 --c="Data Source=LEANDRO\SQLEXPRESS;Initial Catalog=Trader;Integrated Security=True;Pooling=False" --task migrate:down
REM UP
"C:\_leandro\Trade\Trade\packages\FluentMigrator.Console.3.1.3\net461\x64\Migrate.exe" --target=Trader.Migrations.dll --db=SqlServer2014 --c="Data Source=LEANDRO\SQLEXPRESS;Initial Catalog=Trader;Integrated Security=True;Pooling=False" --task migrate:up