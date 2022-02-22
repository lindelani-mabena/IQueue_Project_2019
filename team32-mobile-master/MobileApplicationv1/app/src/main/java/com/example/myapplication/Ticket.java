package com.example.myapplication;

public class Ticket {

    private int Id;
    private  String Token;
    private boolean Active;
    private int Service_Id;
    private int Client_Id;
    private String Status;

    public String getStatus() {
        return Status;
    }

    public void setStatus(String status) {
        Status = status;
    }
// private Date EstimatedTime;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getToken() {
        return Token;
    }

    public void setToken(String token) {
        Token = token;
    }

    public boolean isActive() {
        return Active;
    }

    public void setActive(boolean active) {
        Active = active;
    }

    public int getService_Id() {
        return Service_Id;
    }

    public void setService_Id(int service_Id) {
        Service_Id = service_Id;
    }

    public int getClient_Id() {
        return Client_Id;
    }

    public void setClient_Id(int client_Id) {
        Client_Id = client_Id;
    }
}
