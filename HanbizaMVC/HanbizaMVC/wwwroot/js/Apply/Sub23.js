

const today = new Date();
const setCalendarData = (year, month) => {
    let calHtml = "";
    const setDate = new Date(year, month - 1, 1);   // month = 0 ~ 11
  //  const firstDay = setDate.getDate(); // 1~31 일
    const firstDayName = setDate.getDay();
    const lastDay = new Date(
        setDate.getFullYear(),
        setDate.getMonth() + 1,
        0
    ).getDate();
    const prevLastDay = new Date(
        setDate.getFullYear(),
        setDate.getMonth(),
        0
    ).getDate();

    let startDayCount = 1;
    let lastDayCount = 1;

    for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 7; j++) {
            if (i == 0 && j < firstDayName) { // 첫줄 && 이번달 시작 요일 이전
                if (j == 0) { // 일
                    
                    calHtml +=
                        `<div style='background-color:#FFB3BB;' class='calendar__day horizontalGutter'><span>${(prevLastDay - (firstDayName - 1) + j)}</span><span></span></div>`;
                } else if (j == 6) { // 토
                    
                    calHtml +=
                        `<div style='background-color:#FFB3BB;' class='calendar__day'><span>${(prevLastDay - (firstDayName - 1) + j)}</span><span></span></div>`;
                } else { // 월~금
                    
                    calHtml +=
                        `<div style='background-color:#FFB3BB;' class='calendar__day horizontalGutter'><span>${(prevLastDay - (firstDayName - 1) + j)}</span><span></span></div>`;
                }
            }
            else if (i == 0 && j == firstDayName) { // 첫줄 && 이번 달 시작 요일
                if (j == 0) {
                    
                    calHtml +=
                        `<div style='background-color:#FFE0BB;' class='calendar__day horizontalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                } else if (j == 6) {
                    
                    calHtml +=
                        `<div style='background-color:#FFE0BB;' class='calendar__day'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                } else {
                    
                    calHtml +=
                        `<div style='background-color:#FFE0BB;' class='calendar__day horizontalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                }
            }
            else if (i == 0 && j > firstDayName) { // 첫줄 && 이번 달 시작 이후
                if (j == 0) {
                    
                    calHtml +=
                        `<div style='background-color:#FFFFBB' class='calendar__day horizontalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                } else if (j == 6) {
                    
                    calHtml +=
                        `<div style='background-color:#FFFFBB' class='calendar__day'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                } else {
                    
                    calHtml +=
                        `<div style='background-color:#FFFFBB' class='calendar__day horizontalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                }
            }
            else if (i > 0 && startDayCount <= lastDay) { // 이번달 마지막 날 까지
                if (j == 0) {
                    
                    calHtml +=
                        `<div style='background-color:#BBFFC9;'class='calendar__day horizontalGutter verticalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                } else if (j == 6) {
                    
                    calHtml +=
                        `<div style='background-color:#BBFFC9;'class='calendar__day verticalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                } else {
                    
                    calHtml +=
                        `<div style='background-color:#BBFFC9;'class='calendar__day horizontalGutter verticalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                }
            }
            else if (startDayCount > lastDay) { // 이번달 마지막 날 이후
                if (j == 0) {
                    
                    calHtml +=
                        `<div style='background-color:#B9E1FF;' class='calendar__day horizontalGutter verticalGutter'><span>${lastDayCount++}</span><span></span></div>`;
                } else if (j == 6) {
                    
                    calHtml +=
                        `<div style='background-color:#B9E1FF;' class='calendar__day verticalGutter'><span>${lastDayCount++}</span><span></span></div>`;
                } else {
                    
                    calHtml +=
                        `<div style='background-color:#B9E1FF;' class='calendar__day horizontalGutter verticalGutter'><span>${lastDayCount++}</span><span></span></div>`;
                }
            }
        }
    }
    document
        .querySelector("#calendar")
        .insertAdjacentHTML("beforeend", calHtml);
};

const setFixDayCount = number => {
    //캘린더 하루마다 아이디를 만들어주기 위해 사용
    let fixNum = "";
    if (number < 10) {
        fixNum = "0" + number;
    } else {
        fixNum = number;
    }
    return fixNum;
};

if (today.getMonth() + 1 < 10) {
    setCalendarData(today.getFullYear(), "0" + (today.getMonth() + 1));
} else {
    setCalendarData(today.getFullYear(), "" + (today.getMonth() + 1));
}

function changeYear(flag) {

    let year = $("#year").text();
    let month = $("#month").text();

    year = parseInt(year);
    month = parseInt(month);
    //alert(year + "/" + month);
    if (flag == '+') {
        if (month == 12) {
            year += 1;
            month = 1;
        } else {
            month += 1;
        }
    } else {
        if (month == 1) {
            year -= 1;
            month = 12;
        } else {
            month -= 1;
        }
    }
    $("#calendar").html("");
    if (month < 10) {
        month = "0" + month;
    } else {
        month = "" + month;
    }

    $("#year").text(year);
    $("#month").text(month);
    //alert(year + "/" + month);

    setCalendarData(year, month)
}

$(".calendar__day").click(function () {
    let Click_Date = ''
})
