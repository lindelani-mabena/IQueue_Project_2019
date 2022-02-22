package com.example.myapplication;

public class User {

    private String FirstName;
    private String LastName;

    public String getFirstName() {
        return FirstName;
    }

    public String getLastName() {
        return LastName;
    }

    public String getEmail() {
        return Email;
    }

    public String[] getAddresses() {
        return Addresses;
    }

    public String[] getTickets() {
        return Tickets;
    }

    public String getDob() {
        return Dob;
    }

    public String getPhone() {
        return Phone;
    }

    public boolean isAccountActive() {
        return AccountActive;
    }

    public boolean isOnline() {
        return Online;
    }

    public int getLoginCount() {
        return LoginCount;
    }

    public String getInitialDate() {
        return InitialDate;
    }

    public String getLastUpdate() {
        return LastUpdate;
    }

    public User(String firstName, String lastName, String email, String[] addresses, String[] tickets, String dob, String phone, boolean accountActive, boolean online, int loginCount, String initialDate, String lastUpdate) {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Addresses = addresses;
        Tickets = tickets;
        Dob = dob;
        Phone = phone;
        AccountActive = accountActive;
        Online = online;
        LoginCount = loginCount;
        InitialDate = initialDate;
        LastUpdate = lastUpdate;
    }
    private String Email;
    private String[] Addresses;
    private String[] Tickets;
    private String Dob;
    private String Phone;
    private boolean AccountActive;
    private boolean Online;
    private int LoginCount;
    private String InitialDate;
    private String LastUpdate;

}
