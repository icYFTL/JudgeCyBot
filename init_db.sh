#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "postgres" --dbname "postgres" <<-EOSQL
    CREATE USER judge_slave WITH PASSWORD 'judge';
    CREATE DATABASE judgebot WITH OWNER 'judge_slave';
EOSQL