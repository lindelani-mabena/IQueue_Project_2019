package com.example.myapplication.data;

import java.util.Calendar;
import java.util.Date;
import java.util.StringTokenizer;

public class DateHelper {
    public static Date convertToDateTime(String strInputDateTime) {
        StringTokenizer stringTokenizer = new StringTokenizer(strInputDateTime, "T");
        String strDate = stringTokenizer.nextToken();
        String strTime = stringTokenizer.nextToken();

        stringTokenizer = new StringTokenizer(strDate, "-");

        String strYear = stringTokenizer.nextToken();
        String strMonth = stringTokenizer.nextToken();
        String strDay = stringTokenizer.nextToken();

        int intYear = Integer.parseInt(strYear);
        int intMonth = Integer.parseInt(strMonth) - 1;
        int intDay = Integer.parseInt(strDay);

        stringTokenizer = new StringTokenizer(strTime, ":");

        String strHours = stringTokenizer.nextToken();
        String strMinutes = stringTokenizer.nextToken();
        String strSeconds = stringTokenizer.nextToken();

        int intHours = Integer.parseInt(strHours);
        int intMinutes = Integer.parseInt(strMinutes);

        stringTokenizer = new StringTokenizer(strSeconds, ".");

        strSeconds = stringTokenizer.nextToken();

        int intSeconds = Integer.parseInt(strSeconds);

        Calendar calendar = Calendar.getInstance();
        calendar.set(intYear, intMonth, intDay, intHours, intMinutes, intSeconds);

        return calendar.getTime();
    }

    public static Time convertToTime(String strInputTime) {
        StringTokenizer stringTokenizer = new StringTokenizer(strInputTime, ":");
        String strHours = stringTokenizer.nextToken();
        String strMinutes = stringTokenizer.nextToken();
        String strSeconds = stringTokenizer.nextToken();

        int intHours = Integer.parseInt(strHours);
        int intMinutes = Integer.parseInt(strMinutes);

        stringTokenizer = new StringTokenizer(strSeconds, ".");

        strSeconds = stringTokenizer.nextToken();

        int intSeconds = Integer.parseInt(strSeconds);

        Time time = new Time(intHours, intMinutes, intSeconds);

        return time;
    }
}
