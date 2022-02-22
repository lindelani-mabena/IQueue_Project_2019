package com.example.myapplication.data.model;

import android.util.JsonReader;

import com.example.myapplication.data.model.Branch;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class Service {
    private int id;
    private String name;
    private String description;
    private List<Branch> branches;
    private List<Ticket> tickets;

    public Service(int Id, String Name, String Description, List<Branch> Branches,
                   List<Ticket> Tickets) {
        id = Id;
        name = Name;
        description = Description;
        branches = Branches;
        tickets = Tickets;
    }

    public Service(JSONObject jsonObject) {
        try {
            id = jsonObject.getInt("Id");
            name = jsonObject.getString("Name");
            description = jsonObject.getString("Description");

            JSONArray temp = jsonObject.getJSONArray("Branches");
            branches = new ArrayList<>();
            for(int i = 0; i < temp.length(); i++) {
                JSONObject a = temp.getJSONObject(i);
                Branch branch = new Branch(a);
                branches.add(branch);
            }

            temp = jsonObject.getJSONArray("Tickets");
            tickets = new ArrayList<>();
            for(int i = 0; i < temp.length(); i++) {
                JSONObject a = temp.getJSONObject(i);
                Ticket ticket = new Ticket(a);
                tickets.add(ticket);
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public List<Branch> getBranches() {
        return branches;
    }

    public void setBranches(List<Branch> branches) {
        this.branches = branches;
    }

    public List<Ticket> getTickets() {
        return tickets;
    }

    public void setTickets(List<Ticket> tickets) {
        this.tickets = tickets;
    }

    @Override
    public String toString() {
        return "Service{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", description='" + description + '\'' +
                ", branches=" + branches.toString() +
                ", tickets=" + tickets.toString() +
                '}';
    }
}
