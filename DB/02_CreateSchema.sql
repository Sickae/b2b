create table application (id  serial, in_game_name varchar(255), form_json varchar(255), status varchar(255), status_complete_date_utc timestamp, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create table application_flow (id  serial, description text, description_json text, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create table audit_log (id  serial, User_id int4, related_entity_id int4, operation_type varchar(255), related_entity_json text, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create table blacklist (id  serial, in_game_name varchar(255) unique, reason varchar(255), AddedBy_id int4, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create table user_claim (id  serial, claim_type varchar(255), claim_value varchar(255), User_id int4, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create table "user" (id  serial, in_game_name varchar(255), application_json text, ApplicationFlow_id int4, is_activated boolean, user_name varchar(255), password_hash varchar(255), security_stamp varchar(255), lockout_enabled boolean, access_failed_count int4, lockout_end timestamp, is_deleted boolean, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
alter table audit_log add constraint FK_BB885A59 foreign key (User_id) references "user";
alter table blacklist add constraint FK_11FC188E foreign key (AddedBy_id) references "user";
alter table user_claim add constraint FK_B7CA19B1 foreign key (User_id) references "user";
alter table "user" add constraint FK_B314196C foreign key (ApplicationFlow_id) references application_flow