create table "user" (id  serial, user_name varchar(255), password_hash varchar(255), in_game_name varchar(255), is_deleted boolean, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id))