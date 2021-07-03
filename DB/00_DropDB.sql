-- Postgresql 9.4
select pg_terminate_backend(pid) from pg_stat_activity where datname=:dat_name;

drop database :db_name;