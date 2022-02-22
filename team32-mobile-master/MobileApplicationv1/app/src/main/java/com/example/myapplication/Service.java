package com.example.myapplication;

import java.util.ArrayList;

public class Service {

    private int Id;
    private String Name;
    private String Description;
    private int Brranch_Id;

    public int getId() {
        return Id;
    }

    public String getName() {
        return Name;
    }

    public String getDescription() {
        return Description;
    }

    public int getBrranch_Id() {
        return Brranch_Id;
    }

    public ArrayList<Ticket> Tickets;
}
