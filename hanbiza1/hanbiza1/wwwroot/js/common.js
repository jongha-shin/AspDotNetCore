/**
 * gnb menu 
 * 
 */
var navFun = {
    init: function() {
        this.navmo();
    },
    navmo: function() {
        var open_b = $('.nav-top .more-btn');
        var close_b = $('.nav-top .close-btn');
        var gnb_bg = $('.gnb-bg');
        var nav_box = $('.nav-box');
        var nav_sub = $('.nav-box .list-li');
        var nav_subsub = $('.nav-box .depth-1 ul');
        var nav_subsub_3 = $('.nav-box .depth-2 ul');
        var nav_subsub_a = $('.nav-box .two-dep');
            
        var win_h = $('body').height();
        gnb_bg.css({height:win_h});
        nav_box.hide();
        gnb_bg.hide();
        open_b.on('click', onOver);
        nav_sub.on('click', onSub);
        nav_subsub_a.on('click', onSub_02);
        close_b.on('click', onClo);
        gnb_bg.on('click',onBg);

        function onOver(){
            // open_b.hide();
            // close_b.show();

            if(nav_box.css('display') === 'none'){
              nav_box.show();
              nav_box.animate({'margin-right':'0'}, 200 );

              gnb_bg.show();
              // gnb_bg.css({height:win_h});

              $('.more-btn img').attr('alt','전체메뉴 닫기').attr('src','../images/main/gnb-close.png');

            }else{
              nav_box.animate({'margin-right':'-100%'}, 200, function(){

                  nav_box.hide();
              });
              gnb_bg.fadeOut(100);
              nav_sub.addClass('down').removeClass('up');
              nav_subsub.slideUp(100);

              $('.more-btn img').attr('alt','전체메뉴 열기').attr('src','../images/main/gnb-button.png');

            }

            
        }

        function onClo(){
            // open_b.show();
            // close_b.hide();

            nav_box.animate({'margin-right':'-100%'}, 200, function(){

                nav_box.hide();
            });
            gnb_bg.fadeOut(100);
            nav_sub.addClass('down').removeClass('up');
            nav_subsub.slideUp(100);
        }

        function onBg(){
            // open_b.show();
            // close_b.hide();

            nav_box.animate({'margin-right':'-100%'}, 200, function(){

                nav_box.hide();
            });
            gnb_bg.fadeOut(100);
            nav_sub.addClass('down').removeClass('up');
            nav_subsub.slideUp(100);

            $('.more-btn img').attr('alt','전체메뉴 열기').attr('src','../images/main/gnb-button.png');
        }

        function onSub(){
            nav_sub.addClass('down').removeClass('up');
            if ( $(this).next('ul').css('display') === 'none') {
                nav_subsub.slideUp(100);
                $(this).next().slideDown(200).show();
                $(this).addClass('up').removeClass('down');
            } else{
                $(this).next('ul').slideUp(100);
                nav_sub.addClass('down').removeClass('up');
            
            }

        }

        /* 3depth*/
        function onSub_02(){
            // nav_sub.addClass('down').removeClass('up');
            if ( $(this).next('ul').css('display') === 'none') {
                nav_subsub_3.slideUp(100);
                $(this).next().slideDown(200).show();
            } else{
                $(this).next('ul').slideUp(100);
            
            }

        }
    }
};





/**
 * bxslider 
 */
/*
 var proSlider = {
  init: function() {
    this.slider_01();
  },
  slider_01: function() {
    $('.m-visual-box .bxslider').bxSlider({
      mode: 'horizontal',
      captions: true,
      auto: true,
      autoControls: false,
      controls: true,
      startText: '재생하기',
      stopText: '멈추기',
      autoHover:true,
      touchEnabled:true,
      pager:false

    });
  }
};
*/


/**
 * tab.js
 * 탭을 클릭하면 컨텐츠가 보이고 숨기게 하기
 *
 */
var tabFun = {
    init: function() {
        this.tabShowHide();
        this.tabAjax();
    },
    tabShowHide: function() {
        var tab = $('.tab-movie .tab a'),
            tab_content = $('.tab-movie .tab-content'),
            tab_wrap = $('.tab-movie');

        // 페이지에서 첫번째 .tab-content 보이기
        tab_wrap.each(function() {
            $(this).children('.tab-content').first().show();
        });


        tab.on('click', function(event) {
            var idx = $(this).data('index');
            // this 의 형제 앵커들 active 클래스 제거
            $(this).parent().siblings().children('a').removeClass('active');
            // this 에 active 클래스 추가
            $(this).addClass('active');
            // this 의 부모인 ul 형제들중에서 tab-content 숨기기
            $(this).parents('ul').siblings('.tab-content').hide();
            // this 와 같은 아이디만 보이기
            $('#' + idx).show();

        });
    },
    tabAjax: function() {
        var containerId = '#tabs-container';
        var tabsId = '#tabs';

        $(document).ready(function(){
            // Preload tab on page load
            if($(tabsId + ' li.current a').length > 0){
                loadTab($(tabsId + ' li.current a'));
            }
            
            $(tabsId + ' a').click(function(){
                if($(this).parent().hasClass('current')){ return false; }
                
                $(tabsId + ' li.current').removeClass('current');
                $(this).parent().addClass('current');
                
                loadTab($(this));       
                return false;
            });
        });

        function loadTab(tabObj){
            if(!tabObj || !tabObj.length){ return; }
            $(containerId).addClass('loading');
            // $(containerId).fadeOut('fast');
            
            $(containerId).load(tabObj.attr('href'), function(){
                $(containerId).removeClass('loading');
                // $(containerId).fadeIn('fast');
            });
        }
    }
};



 /**
  * 스크롤바 생성
  */
var mainScroll = {
    init: function() {
        this.Scroll_01();
    },
    Scroll_01: function() {

        var m_scroll_w = $('body').width();
        
        if(m_scroll_w < 639){
            $('.scroll-con .m-scroll').mCustomScrollbar({
                axis:'x',
                advanced:{autoExpandHorizontalScroll:true}
            });
        }else{}
        
    }
};



 /**
  * 스크롤바 생성
  */
var listdown = {
    init: function() {
        this.list_01();
    },
    list_01: function() {

        var list_link = $('.list-down-js .list-down-link');
        var list_con =$('.list-down-js .list-down-con');
        
        list_con.hide();
        list_link.on('click', downshow);

        function downshow(){
          if( $(this).siblings('.list-down-con').css('display') === 'none'){
            list_link.removeClass('active');
            list_con.slideUp(200);
            $(this).addClass('active').siblings('.list-down-con').slideDown(200);
          }else{
            $(this).removeClass('active').siblings('.list-down-con').slideUp(200);
          }
          
        }
        
        
    }
};

 /**
  * 클릭 생성
  */
var openjs = {
    init: function() {
        this.list_01();
    },
    list_01: function() {

        var open_link = $('.open-js .link-js');
        var open_con =$('.open-con-js');
        var close_link =$('.open-js .close-btn');
        
        open_con.hide();
        open_link.on('click', openshow);

        function openshow(){
          if( $(this).siblings('.open-con-js').css('display') === 'none'){
            close_link.show();
            open_con.fadeIn(200);

          }else{
            close_link.hide();
            open_con.fadeOut(200);
          }
          
        }
        
        
    }
};

 /**
  * 클릭 생성
  */
var searchjs = {
    init: function() {
        this.list_01();
    },
    list_01: function() {

        var search_link = $('.search-js .search-link-js');
        var search_con_01 =$('.search-js .search-con-js');
        var search_con_02 =$('.search-js .search-con-js-02');
        var close_link =$('.search-js .close-js');
        
        search_link.on('click', searchshow);
        close_link.on('click', searchshow_02)

        function searchshow(){
          search_con_01.hide();
          search_con_02.show();   
        }

        function searchshow_02(){
          search_con_01.show();
          search_con_02.hide();   
        }
        
        
    }
};

/*
$(document).ready(function(e){

  tabFun.init();
  navFun.init();
  proSlider.init(); 
  mainScroll.init(); 
  listdown.init();
  openjs.init();
  searchjs.init();
*/

  //셀렉트 링크
  $('.select-tabmenu').change(function(event) {
    var url = $(this).val();
    if ($(this).hasClass('move-page')) window.location.href = url;
  });




  /**
   * smooth scroll
   * @return {[type]} [description]
   */
  // $(function() {
  //     $('a[href*=#]:not([href=#])').click(function() {
  //         if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
  //             var target = $(this.hash);
  //             target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
  //             if (target.length) {
  //                 $('html,body').animate({
  //                     scrollTop: target.offset().top
  //                 }, 500);
  //             return false;
  //             }
  //         }
  //     });
  // });

  $('.scroll-link').click(function() {
        var ScrollOffset = $(this).attr('data-soffset');
        $('html, body').animate({
            scrollTop: $($(this).attr('href')).offset().top-ScrollOffset - 70 + 'px'
        }, {
            duration: 1000,
            easing: 'swing'
        });
        return false;
    });

  
  
  // top키 스크롤

  //$().UItoTop({ 
  //  easingType: 'easeOutQuart',
  //  min:150
  //});

  // bottom키 스크롤

  //$().UItoBottom({ 
  //  easingType: 'easeOutQuart',
  //  max:550
  //});


  // 효과 주기
$('.header-fixed').scrollToFixed();

    
//});


function OnCheckPhone(oTa) {
    var oForm = oTa.form ;
    var sMsg = oTa.value ;
    var onlynum = "" ;
    var imsi=0;
    onlynum = RemoveDash2(sMsg);  //하이픈 입력시 자동으로 삭제함
    onlynum =  checkDigit(onlynum);  // 숫자만 입력받게 함
    var retValue = "";

	if(event.keyCode != 12 ) {
		if(onlynum.substring(0,2) == 02) {  // 서울전화번호일 경우  10자리까지만 나타나고 그 이상의 자리수는 자동삭제
			if (GetMsgLen(onlynum) <= 1) oTa.value = onlynum ;
			if (GetMsgLen(onlynum) == 2) oTa.value = onlynum + "-";
			if (GetMsgLen(onlynum) == 4) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,3) ;
			if (GetMsgLen(onlynum) == 4) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,4) ;
			if (GetMsgLen(onlynum) == 5) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,5) ;
			if (GetMsgLen(onlynum) == 6) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,6) ;
			if (GetMsgLen(onlynum) == 7) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,5) + "-" + onlynum.substring(5,7) ; ;
			if (GetMsgLen(onlynum) == 8) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,6) + "-" + onlynum.substring(6,8) ;
			if (GetMsgLen(onlynum) == 9) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,5) + "-" + onlynum.substring(5,9) ;
			if (GetMsgLen(onlynum) == 10) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,6) + "-" + onlynum.substring(6,10) ;
			if (GetMsgLen(onlynum) == 11) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,6) + "-" + onlynum.substring(6,10) ;
			if (GetMsgLen(onlynum) == 12) oTa.value = onlynum.substring(0,2) + "-" + onlynum.substring(2,6) + "-" + onlynum.substring(6,10) ;
		}
		if(onlynum.substring(0,2) == 05 ) {  // 05로 시작되는 번호 체크
			if(onlynum.substring(2,3) == 0 ) {  // 050으로 시작되는지 따지기 위한 조건문
				if (GetMsgLen(onlynum) <= 3) oTa.value = onlynum ;
				if (GetMsgLen(onlynum) == 4) oTa.value = onlynum + "-";
				if (GetMsgLen(onlynum) == 5) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,5) ;
				if (GetMsgLen(onlynum) == 6) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,6) ;
				if (GetMsgLen(onlynum) == 7) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,7) ;
				if (GetMsgLen(onlynum) == 8) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) ;
				if (GetMsgLen(onlynum) == 9) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,7) + "-" + onlynum.substring(7,9) ; ;
				if (GetMsgLen(onlynum) == 10) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) + "-" + onlynum.substring(8,10) ;
				if (GetMsgLen(onlynum) == 11) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,7) + "-" + onlynum.substring(7,11) ;
				if (GetMsgLen(onlynum) == 12) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) + "-" + onlynum.substring(8,12) ;
				if (GetMsgLen(onlynum) == 13) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) + "-" + onlynum.substring(8,12) ;
		   } else {
				if (GetMsgLen(onlynum) <= 2) oTa.value = onlynum ;
				if (GetMsgLen(onlynum) == 3) oTa.value = onlynum + "-";
				if (GetMsgLen(onlynum) == 4) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,4) ;
				if (GetMsgLen(onlynum) == 5) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,5) ;
				if (GetMsgLen(onlynum) == 6) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) ;
				if (GetMsgLen(onlynum) == 7) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) ;
				if (GetMsgLen(onlynum) == 8) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) + "-" + onlynum.substring(6,8) ; ;
				if (GetMsgLen(onlynum) == 9) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,9) ;
				if (GetMsgLen(onlynum) == 10) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) + "-" + onlynum.substring(6,10) ;
				if (GetMsgLen(onlynum) == 11) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,11) ;
				if (GetMsgLen(onlynum) == 12) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,11) ;
			}
		}

        if(onlynum.substring(0,2) == 03 || onlynum.substring(0,2) == 04  || onlynum.substring(0,2) == 06  || onlynum.substring(0,2) == 07  || onlynum.substring(0,2) == 08 ) {  // 서울전화번호가 아닌 번호일 경우(070,080포함 // 050번호가 문제군요)
            if (GetMsgLen(onlynum) <= 2) oTa.value = onlynum ;
            if (GetMsgLen(onlynum) == 3) oTa.value = onlynum + "-";
            if (GetMsgLen(onlynum) == 4) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,4) ;
            if (GetMsgLen(onlynum) == 5) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,5) ;
            if (GetMsgLen(onlynum) == 6) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) ;
            if (GetMsgLen(onlynum) == 7) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) ;
            if (GetMsgLen(onlynum) == 8) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) + "-" + onlynum.substring(6,8) ; ;
            if (GetMsgLen(onlynum) == 9) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,9) ;
            if (GetMsgLen(onlynum) == 10) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) + "-" + onlynum.substring(6,10) ;
            if (GetMsgLen(onlynum) == 11) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,11) ;
            if (GetMsgLen(onlynum) == 12) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,11) ;
        }
        if(onlynum.substring(0,2) == 01) {  //휴대폰일 경우
            if (GetMsgLen(onlynum) <= 2) oTa.value = onlynum ;
            if (GetMsgLen(onlynum) == 3) oTa.value = onlynum + "-";
            if (GetMsgLen(onlynum) == 4) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,4) ;
            if (GetMsgLen(onlynum) == 5) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,5) ;
            if (GetMsgLen(onlynum) == 6) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) ;
            if (GetMsgLen(onlynum) == 7) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) ;
            if (GetMsgLen(onlynum) == 8) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,8) ;
            if (GetMsgLen(onlynum) == 9) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,9) ;
            if (GetMsgLen(onlynum) == 10) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,6) + "-" + onlynum.substring(6,10) ;
            if (GetMsgLen(onlynum) == 11) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,11) ;
            if (GetMsgLen(onlynum) == 12) oTa.value = onlynum.substring(0,3) + "-" + onlynum.substring(3,7) + "-" + onlynum.substring(7,11) ;
        }
        if(onlynum.substring(0,1) == 1) {  // 1588, 1688등의 번호일 경우
            if (GetMsgLen(onlynum) <= 3) oTa.value = onlynum ;
            if (GetMsgLen(onlynum) == 4) oTa.value = onlynum + "-";
            if (GetMsgLen(onlynum) == 5) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,5) ;
            if (GetMsgLen(onlynum) == 6) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,6) ;
            if (GetMsgLen(onlynum) == 7) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,7) ;
            if (GetMsgLen(onlynum) == 8) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) ;
            if (GetMsgLen(onlynum) == 9) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) ;
            if (GetMsgLen(onlynum) == 10) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) ;
            if (GetMsgLen(onlynum) == 11) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) ;
            if (GetMsgLen(onlynum) == 12) oTa.value = onlynum.substring(0,4) + "-" + onlynum.substring(4,8) ;
        }
    }
}

function RemoveDash2(sNo) {
    var reNo = ""
    for(var i=0; i<sNo.length; i++) {
        if ( sNo.charAt(i) != "-" ) {
            reNo += sNo.charAt(i);
        }
    }
    return reNo
}

function GetMsgLen(sMsg) { // 0-127 1byte, 128~ 2byte
    var count = 0;
    for(var i=0; i<sMsg.length; i++) {
        if ( sMsg.charCodeAt(i) > 127 ) {
            count += 2;
        }    else {
            count++;
        }
    }
    return count
}

// 세자리 콤마 찍기
function makeCurrency(obj) { 
          var tmp = new Array(); 
          var c = 1; 
          var coma = ','; 
          var val = obj.value, val2 = ""; 
          var len = val.length; 

          for(i = len; i > -1; i--) { 
                    if(val.charAt(i) != coma) { 
                              val2 = val.charAt(i) + val2; 
                    } 
          } 
           
          val = val2; 
          len = val.length; 

          for(i = len; i > -1; i--) { 
                    c++; 

                    if((c % 3 == 0) && (i != len - 1)) { 
                              tmp[i] = val.charAt(i) + coma; 
                    } else { 
                              tmp[i] = val.charAt(i); 
                    } 
          } 
          obj.value = tmp.join(''); 
} 

// 숫자만 입력
function chkNum(key) {

    if ((key >= 48 && key <= 57) // 키보드 상단 숫자키
       || (key >= 96 && key <= 105) // 키패드 숫자키
       || key == 8  // 백스페이스 키
       || key == 37 // 왼쪽 화살표 키
       || key == 39 // 오른쪽 화살표 키
       || key == 46 // DEL 키
       || key == 13 // 엔터 키
       || key == 9  // Tab 키
       ) {
           return ;
      } else {
           alert("숫자만 입력 가능합니다.");
          return ;
     }
}

// 숫자 체크
function checkNum(key)
{
    if ((key >= 48 && key <= 57)    || (key >= 96 && key <= 105)   || key == 8  || key == 37  || key == 39  || key == 46  || key == 13 || key == 9 )
    {
           return true;
      } else {
          alert("Please just enter the number!");
          return false;
     }
}

//숫자만 입력 onKeyUp
function num_only1(obj){
  if((event.keyCode<48) || (event.keyCode>57)){
	if (obj.value.length==1)
	{
		obj.value='';
	}else{
		obj.value=obj.value.substring(0,obj.value.length-1);
   }
    alert('숫자만 입력가능합니다.');
  }
}

//숫자만 입력 onKeyPress
function num_only(){
  if((event.keyCode<48) || (event.keyCode>57)){
    event.returnValue=false;
  }
}

//숫자만 입력 onChange
function checkNumber(){ 
	var objEv = event.srcElement; 
	var numPattern = /([^0-9])/; 
	numPattern = objEv.value.match(numPattern); 
	if(numPattern != null){ 
		alert("숫자만 입력해 주세요!"); 
		objEv.value=""; 
		objEv.focus(); 
		return false; 
	} 
} 

function checkDigit(num) {
    var Digit = "1234567890";
    var string = num;
    var len = string.length
    var retVal = "";

    for (i = 0; i < len; i++)
    {
        if (Digit.indexOf(string.substring(i, i+1)) >= 0)
        {
            retVal = retVal + string.substring(i, i+1);
        }
    }
    return retVal;
}
/*전화번호 입력시 자동하이픈 끝*/

/*날짜타입의 숫자 입력시 -가 자동으로 추가
<input type="text" id="date" onkeyup="auto_date_format(event, this)" onkeypress="auto_date_format(event, this)" maxlength="10" />
*/
function auto_date_format( e, oThis ){
	var num_arr = [ 
            97, 98, 99, 100, 101, 102, 103, 104, 105, 96,
            48, 49, 50, 51, 52, 53, 54, 55, 56, 57
        ]
        var key_code = ( e.which ) ? e.which : e.keyCode;
        if( num_arr.indexOf( Number( key_code ) ) != -1 ){
        
            var len = oThis.value.length;
            if( len == 4 ) oThis.value += "-";
            if( len == 7 ) oThis.value += "-";
        }
        
}

//숫자 + 콤만 입력 클래스 num_only(숫자만), num_only2(숫자 + 콤마)
$(document).ready(function(){ 
    $('.num_only').css('imeMode','disabled').keypress(function(event) {
        if(event.which && (event.which < 48 || event.which > 57) ) {
 			alert("Please just enter the number!");
           event.preventDefault();
        }
    }).keyup(function(){
        if( $(this).val() != null && $(this).val() != '' ) {
            $(this).val( $(this).val().replace(/[^0-9]/g, '') );
        }
    });
    $('.num_only2').css('imeMode','disabled').keypress(function(event) {
        if(event.which && (event.which < 48 || event.which > 57) ) {
            event.preventDefault();
			alert("Please just enter the number!");
        }
    }).keyup(function(){
        if( $(this).val() != null && $(this).val() != '' ) {
            var tmps = $(this).val().replace(/[^0-9]/g, '');
            var tmps2 = tmps.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
            $(this).val(tmps2);
    }
    });
});








