package com.example.myapplication;

import com.google.gson.annotations.SerializedName;

public class Client {

    @SerializedName("Email")
    private String Email;
    @SerializedName("Password")
    private String Password;
    @SerializedName("ConfirmPassword")
    private String ConfirmPassword;

    public Client(String emailAddress, String password, String confirmPassword) {
        Email = emailAddress;
        Password = password;
        ConfirmPassword = confirmPassword;
    }

    public String getEmailAddress() {
        return Email;
    }

    public void setEmailAddress(String emailAddress) {
        Email = emailAddress;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }
    public String getConfirmPassword() {
        return ConfirmPassword;
    }

    public void setConfirmPassword(String confirmPassword) {
        ConfirmPassword = confirmPassword;
    }


}
