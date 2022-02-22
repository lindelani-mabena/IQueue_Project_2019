package com.example.myapplication;

public class OnlineUser {

    private String Name;
    private String LastName;

    public OnlineUser(String name, String lastName, String access_Token, String title, String phoneNumber) {
        Name = name;
        LastName = lastName;
        Access_Token = access_Token;
        Title = title;
        PhoneNumber = phoneNumber;
    }

    private String Access_Token;

    public String getAccess_Token() {
        return Access_Token;
    }

    public void setAccess_Token(String access_Token) {
        Access_Token = access_Token;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getLastName() {
        return LastName;
    }

    public void setLastName(String lastName) {
        LastName = lastName;
    }

    public String getTitle() {
        return Title;
    }

    public void setTitle(String title) {
        Title = title;
    }

    public String getPhoneNumber() {
        return PhoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        PhoneNumber = phoneNumber;
    }

    private String Title;
    private String PhoneNumber;
}
