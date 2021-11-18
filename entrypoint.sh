#!/bin/bash

ls
set -e
run_cmd="dotnet Ozon.Route256.MerchandiseService.dll --no-build -v d"

dotnet Ozon.Route256.MerchandiseService.Migrator.dll --no-build -v d -- --dryrun

dotnet Ozon.Route256.MerchandiseService.Migrator.dll --no-build -v d

>&2 echo "MerchandiseService DB Migrations complete, starting app."
>&2 echo "Run MerchandiseService: $run_cmd"
exec $run_cmd