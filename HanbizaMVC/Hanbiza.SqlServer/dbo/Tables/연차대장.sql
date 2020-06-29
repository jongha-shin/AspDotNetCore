CREATE TABLE [dbo].[연차대장] (
    [SEQID]   INT          IDENTITY(1,1) NOT NULL,
    [Dname]   VARCHAR (50) NOT NULL,
    [BizNum]  CHAR (10)    NOT NULL,
    [StaffID] INT          NOT NULL,
    [입사일]     DATE         NOT NULL,
    [연차발생일]   DATE         NULL,
    [근속년수]    FLOAT (53)   NULL,
    [발생연차]    FLOAT (53)   NULL,
    [잔여일수]    FLOAT (53)   NULL,
    [이월조정추가]  FLOAT (53)   NULL,
    [사용기간]    VARCHAR (50) NULL,
    [연차구분]    CHAR (1)     NULL,
    [등록인]     VARCHAR (50) NULL,
    [Regdate] DATETIME     NULL
);

