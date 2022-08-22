-- schema tables
create table USERS (UUID int,NAME varchar(10),PRIMARY KEY(UUID));
create table ROOMS (RUID int,NAME varchar(21),ISGROUP int,PRIMARY KEY(RUID));
create table USER_ROOM (UUID int,RUID int,ISGROUP int,FOREIGN KEY (UUID) REFERENCES USERS(UUID),FOREIGN KEY (RUID) REFERENCES ROOMS(RUID));
create table MESSAGES (UUID int,RUID int,ISGROUP int,time TIMESTAMP,CONTENT varchar(100),FOREIGN KEY (UUID) REFERENCES USERS(UUID),FOREIGN KEY (RUID) REFERENCES ROOMS(RUID),PRIMARY KEY (UUID,RUID,time));

---- INSERTING DATA

-- create users
insert into USERS values(0,'Parth');
insert into USERS values(1,'Arihant');
insert into USERS values(3,'Kushal');
-- create room DM
insert into ROOMS values(0,'Parth-Arihant',0);
-- create room group 
insert into ROOMS values(1,'Sigma',1);
insert into ROOMS values(2,'ITB',1);
-- add person to DM
insert into USER_ROOM values(0,0,0);
insert into USER_ROOM values(1,0,0);
-- add people to group
insert into USER_ROOM values(1,1,1);
insert into USER_ROOM values(1,2,1);
insert into USER_ROOM values(0,2,1);
insert into USER_ROOM values(0,1,1);
insert into USER_ROOM values(3,2,1);
-- send message to DM
insert into MESSAGES values(0,0,0,'23-APR-2022','Hello ari !');
insert into MESSAGES values(1,0,0,'24-APR-2022','Hello partho !');
-- send message to Group
insert into MESSAGES values(0,2,1,'23-APR-2022','Hello ari and kushal !');

---- SELECTING DATA

-- rooms a person belongs to
select * from ROOMS where RUID in ( select RUID from USER_ROOM where UUID = 2);


