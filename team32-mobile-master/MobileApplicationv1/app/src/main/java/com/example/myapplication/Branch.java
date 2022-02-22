package com.example.myapplication;

import java.util.ArrayList;

public class Branch {

    private int Id;

    public int getId() {
        return Id;
    }

    private String Name;
    private String Code;
    private String Phone;
    private String Email;
    public String getName() {
        return Name;
    }
    public void setName(String name) {
        Name = name;
    }
    public ArrayList<Service> Services;
    public Branch(String name, String code, String phone, String email) {
        Name = name;
        Code = code;
        Phone = phone;
        Email = email;
    }
    public String getCode() {
        return Code;
    }

    public void setCode(String code) {
        Code = code;
    }

    public String getPhone() {
        return Phone;
    }

    public void setPhone(String phone) {
        Phone = phone;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }
}
