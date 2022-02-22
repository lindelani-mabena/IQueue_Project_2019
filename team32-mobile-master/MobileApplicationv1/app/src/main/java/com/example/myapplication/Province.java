package com.example.myapplication;

import java.sql.Date;

public class Province {
    private int Province_Id;
    private String Name;
    private Date Last_Update;

    public int getProvince_Id() {
        return Province_Id;
    }

    public String getName() {
        return Name;
    }

    public Date getLast_Update() {
        return Last_Update;
    }

    public Date getInitial_Date() {
        return Initial_Date;
    }

    private Date Initial_Date;

    public Province(int province_Id, String name, Date last_Update, Date initial_Date) {
        Province_Id = province_Id;
        Name = name;
        Last_Update = last_Update;
        Initial_Date = initial_Date;
    }

    public Province(String name) {
        Name = name;
    }
}

