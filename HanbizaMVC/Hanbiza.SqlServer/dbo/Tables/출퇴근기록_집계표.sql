CREATE TABLE [dbo].[출퇴근기록_집계표] (
    [SEQID]   INT          NOT NULL,
    [Dname]   VARCHAR (50) NOT NULL,
    [BizNum]  CHAR (10)    NOT NULL,
    [StaffID] INT          NOT NULL,
    [시작일]     DATE         NOT NULL,
    [종료일]     DATE         NOT NULL,
    [기준일]     INT          NULL,
    [휴무일]     FLOAT (53)   NULL,
    [유급휴일]    FLOAT (53)   NULL,
    [근무일]     FLOAT (53)   NULL,
    [유급휴가_휴일] FLOAT (53)   NULL,
    [무급휴가_휴일] FLOAT (53)   NULL,
    [결근일]     FLOAT (53)   NULL,
    [주휴일]     FLOAT (53)   NULL,
    [유급주휴일]   FLOAT (53)   NULL,
    [등록인]     VARCHAR (50) NULL,
    [regdate] DATETIME     NULL
);

