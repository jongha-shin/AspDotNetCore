

const today = new Date();
const setCalendarData = (year, month, date, ViewType) => {
    let calHtml = "";
    $("#calendar").html(calHtml);
        
    if (ViewType === 'month_Type') {
        const setDate = new Date(year, month - 1, 1);   // month = 0 ~ 11
      //  const firstDay = setDate.getDate(); // 1~31 일
        const firstDayName = setDate.getDay();  // 요일번호가져오기
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
                // 첫줄 && 이번달 시작 요일 이전
                if (i == 0 && j < firstDayName) { 
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
                // 첫줄 && 이번 달 시작 이후
                else if (i == 0 && j >= firstDayName) { 
                    if (j == 0) {
                    
                        calHtml +=
                            `<div style='background-color:#BBFFC9' class='calendar__day horizontalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                    } else if (j == 6) {
                    
                        calHtml +=
                            `<div style='background-color:#BBFFC9' class='calendar__day'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                    } else {
                    
                        calHtml +=
                            `<div style='background-color:#BBFFC9' class='calendar__day horizontalGutter'><span>${startDayCount}</span><span id='${year}${month}${setFixDayCount(startDayCount++)}'></span></div>`;
                    }
                }
                // 이번달 마지막 날 까지
                else if (i > 0 && startDayCount <= lastDay) { 
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
                // 이번달 마지막 날 이후
                else if (startDayCount > lastDay) { 
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

        
    }
    if (ViewType === 'week_Type') {
        const theDayOfWeek = today.getDay();
        const thisWeek = [];
        for (let i = 0; i < 7; i++) {
            let resultDay = new Date(year, month - 1, date + (i - theDayOfWeek));
            let yyyy = resultDay.getFullYear();
            let mm = Number(resultDay.getMonth()) + 1;
            let dd = resultDay.getDate();

            mm = String(mm).length === 1 ? '0' + mm : mm;
            let dd2 = String(dd).length === 1 ? '0' + dd : dd;

            thisWeek[i] = yyyy + '-' + mm + '-' + dd2;
            if (i < 6) {
                calHtml += `<div style='background-color:#BBFFC9;'class='calendar__day horizontalGutter verticalGutter'><span>${dd}</span><span id='${yyyy}${mm}${dd2}'></span></div>`;
            } else {
                calHtml += `<div style='background-color:#BBFFC9;'class='calendar__day verticalGutter'><span>${dd}</span><span id='${yyyy}${mm}${dd2}'></span></div>`;
            }
        }
        //console.log(thisWeek);


    }

    document
        .querySelector("#calendar")
        .insertAdjacentHTML("beforeend", calHtml);

    GetData(2);
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
    setCalendarData(today.getFullYear(), "0" + (today.getMonth() + 1), today.getDate(), 'month_Type');
} else {
    setCalendarData(today.getFullYear(), "" + (today.getMonth() + 1), today.getDate(), 'month_Type');
}



$(document).on("click", ".calendar__day", function () {
    let Gubun1 = $(".Gubun1").val();
    let Gubun2 = $(".Gubun2").val();

    $(".Gubun1").val(Gubun1);
    $(".Gubun2").val(Gubun2);
    let dayID = $(this).find("span:nth-child(2)").attr("id");
    let _dayID = parseDate(dayID);

    $("#ReserveDate").val(_dayID);

    $("#findReserveModal").modal('show')
})

function setCalendarType(ViewType) {
    setCalendarData(today.getFullYear(), "0" + (today.getMonth() + 1), today.getDate(), ViewType)
}



function parseDate(str) {
    var y = str.substr(0, 4);
    var m = str.substr(4, 2);
    var d = str.substr(6, 2);
    var str = y + "-" + m + "-" + d;
    return str;
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
function GetData(flag) {
    if (flag === 1) {
        let Gubun1 = $(".Gubun1").val();
        $(".Gubun1").val(Gubun1);
        $.ajax({
            url: "/Apply/Sub23_1/" + Gubun1,
            type: "GET",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "json",
            success: function (result) {
                let sjson = JSON.parse(result);
                
                $(".Gubun2").empty();
                $(".Gubun2").append("<option>전체보기</option>");
                let html;
                for (let i = 0; i < sjson.length; i++) {
                    html += "<option value=" + sjson[i].SeqID + ">" + sjson[i].Gubun2 + "</option>";
                }
                $(".Gubun2").append(html);
                
            },
            error: function () { alert("통신실패"); }

        });
    }
    if (flag === 2) {
        let Gubun1 = $(".Gubun1 option:selected").val();
        let Gubun2 = $(".Gubun2 option:selected").val();
        let pickDate = $("#ReserveDate").text();
        //alert(`${Gubun1}/${Gubun2}/${pickDate}`);

        //$.ajax({
        //    url: `/Apply/Sub23_2/${Gubun1}/${Gubun2}` ,
        //    type: "GET",
        //    contentType: "application/x-www-form-urlencoded; charset=utf-8",
        //    dataType: "json",
        //    success: function (result) {
        //        let sjson = JSON.parse(result);

                

        //    },
        //    error: function () { alert("통신실패"); }

        //});



    }
}