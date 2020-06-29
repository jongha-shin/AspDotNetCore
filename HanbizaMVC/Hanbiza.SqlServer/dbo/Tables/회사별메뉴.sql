CREATE TABLE [dbo].[회사별메뉴]
(
	[SEQID] INT IDENTITY(1,1) NOT NULL, 
    [BizNum] CHAR(10) NULL, 
    [CompanyName] NVARCHAR(50) NULL, 
    [근태보기] BIT NULL, 
    [OT신청] BIT NULL, 
    [휴가신청] BIT NULL, 
    [휴가결재] BIT NULL, 
    [연차보기] BIT NULL, 
    [급여명세서] BIT NULL, 
    [확인서명] BIT NULL, 
    [내문서] BIT NULL, 
    [비밀번호변경] BIT NULL, 
    [로그아웃] BIT NULL 
)
