create table provinces
(
    Id           varchar(36)  not null
        primary key,
    Name         varchar(100) not null,
    CreatedDate  datetime     null,
    ModifiedDate datetime     null,
    CreatedName  varchar(255) null,
    ModifiedName varchar(255) null
);

create table districts
(
    Id           varchar(36)  not null
        primary key,
    ProvinceId   varchar(36)  not null,
    Name         varchar(100) not null,
    CreatedDate  datetime     null,
    ModifiedDate datetime     null,
    CreatedName  varchar(255) null,
    ModifiedName varchar(255) null,
    constraint districts_provinces_FK
        foreign key (ProvinceId) references provinces (Id)
);

create table users
(
    Id           varchar(36)  not null
        primary key,
    FullName     varchar(255) null,
    UserName     varchar(100) null,
    Password     varchar(50)  null,
    Email        varchar(100) null,
    PhoneNumber  varchar(20)  null,
    DistrictId   varchar(36)  null,
    Address      varchar(255) null,
    CreatedDate  datetime     null,
    ModifiedDate datetime     null,
    CreatedName  varchar(255) null,
    ModifiedName varchar(255) null,
    Role         int          null,
    constraint users_districts_FK
        foreign key (DistrictId) references districts (Id)
);

create table messages
(
    Id           varchar(36)  not null
        primary key,
    SenderId     varchar(36)  null,
    Content      varchar(255) null,
    CreatedDate  datetime     null,
    ModifiedDate datetime     null,
    CreatedName  varchar(255) null,
    ModifiedName varchar(255) null,
    ReceiverId   varchar(36)  null,
    constraint messages_users_Id_fk
        foreign key (SenderId) references users (Id),
    constraint messages_users_Id_fk_2
        foreign key (ReceiverId) references users (Id)
);

create table realestates
(
    Id             varchar(36)  not null
        primary key,
    OwnerId        varchar(36)  null,
    DistrictId     varchar(36)  null,
    Address        varchar(255) null,
    Latitude       double       null,
    Longtitude     double       null,
    Area           double       null,
    Title          varchar(255) null,
    Name           varchar(255) null,
    Description    longtext     null,
    Price          double       null,
    Feature        varchar(255) null,
    Type           int          null,
    CreatedDate    datetime     null,
    ModifiedDate   datetime     null,
    CreatedName    varchar(255) null,
    ModifiedName   varchar(255) null,
    RealEstateType int          null,
    IsPersonal     tinyint(1)   null,
    IsAccepted     int          null,
    constraint realestates_districts_Id_fk
        foreign key (DistrictId) references districts (Id),
    constraint realestates_users_Id_fk
        foreign key (OwnerId) references users (Id)
);

create table apartments
(
    Id              varchar(36)  not null
        primary key,
    RealEstateId    varchar(36)  null,
    NumberOfBedRoom int          null,
    NumberOfToilet  int          null,
    Floor           int          null,
    Funiture        varchar(255) null,
    LegalDocument   tinyint(1)   null,
    CreatedDate     datetime     null,
    ModifiedDate    datetime     null,
    CreatedName     varchar(255) null,
    ModifiedName    varchar(255) null,
    constraint apartments_realestates_Id_fk
        foreign key (RealEstateId) references realestates (Id)
);

create table boardinghouses
(
    Id            varchar(36)  not null
        primary key,
    RealEstateId  varchar(36)  null,
    Funiture      varchar(255) null,
    SeftContained tinyint(1)   null,
    CreatedDate   datetime     null,
    ModifiedDate  datetime     null,
    CreatedName   varchar(255) null,
    ModifiedName  varchar(255) null,
    constraint boardinghouses_realestates_Id_fk
        foreign key (RealEstateId) references realestates (Id)
);

create table houses
(
    Id              varchar(36)  not null
        primary key,
    RealEstateId    varchar(36)  null,
    NumberOfBedRoom int          null,
    NumberOfToilet  int          null,
    NumberOfFloor   int          null,
    Funiture        varchar(255) null,
    RedBook         tinyint(1)   null,
    CreatedDate     datetime     null,
    ModifiedDate    datetime     null,
    CreatedName     varchar(255) null,
    ModifiedName    varchar(255) null,
    constraint houses_realestates_Id_fk
        foreign key (RealEstateId) references realestates (Id)
);

create table imageurls
(
    Id           varchar(36)  not null
        primary key,
    RealEstateId varchar(36)  not null,
    Url          varchar(255) null,
    CreatedDate  datetime     null,
    ModifiedDate datetime     null,
    CreatedName  varchar(255) null,
    ModifiedName varchar(255) null,
    constraint imageurls_realestates_Id_fk
        foreign key (RealEstateId) references realestates (Id)
);

create table lands
(
    Id            varchar(36)  not null
        primary key,
    RealEstateId  varchar(36)  null,
    LandType      varchar(255) null,
    LegalDocument tinyint(1)   null,
    ModifiedName  varchar(255) null,
    CreatedDate   datetime     null,
    ModifiedDate  datetime     null,
    CreatedName   varchar(255) null,
    constraint lands_realestates_Id_fk
        foreign key (RealEstateId) references realestates (Id)
);

create table postsaves
(
    Id           varchar(36)  not null
        primary key,
    UserId       varchar(36)  null,
    RealEstateId varchar(36)  null,
    CreatedDate  datetime     null,
    ModifiedDate datetime     null,
    CreatedName  varchar(255) null,
    ModifiedName varchar(255) null,
    constraint postsaves_realestates_Id_fk
        foreign key (RealEstateId) references realestates (Id),
    constraint postsaves_users_Id_fk
        foreign key (UserId) references users (Id)
);

