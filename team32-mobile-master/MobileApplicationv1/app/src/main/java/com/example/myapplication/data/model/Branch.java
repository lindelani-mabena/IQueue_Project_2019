package com.example.myapplication.data.model;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class Branch {
    private int id;
    private String name;
    private String code;
    private String email;
    private String cellphone;
    private Address address;
    private List<Service> services;

    public Branch(){}

    public Branch(int id, String name, String code, String email, String cellphone, Address address,
                  List<Service> services) {
        this.id = id;
        this.name = name;
        this.code = code;
        this.email = email;
        this.cellphone = cellphone;
        this.address = address;
        this.services = services;
    }

    public Branch(JSONObject jsonObject) {
        try {
            id = Integer.parseInt(jsonObject.getString("Id"));
            name = jsonObject.getString("Name");
            code = jsonObject.getString("Code");
            email = jsonObject.getString("Email");
            cellphone = jsonObject.getString("Phone");
            JSONObject temp = jsonObject.getJSONObject("Address");
            address = new Address(temp);
            JSONArray a = jsonObject.getJSONArray("Services");
            services = new ArrayList<>();
            for(int i = 0; i < a.length(); i++) {
                JSONObject b = a.getJSONObject(i);
                Service service = new Service(b);
                services.add(service);
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

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getCellphone() {
        return cellphone;
    }

    public void setCellphone(String cellphone) {
        this.cellphone = cellphone;
    }

    public Address getAddress() {
        return address;
    }

    public void setAddress(Address address) {
        this.address = address;
    }

    public List<Service> getServices() {
        return services;
    }

    public void setServices(List<Service> services) {
        this.services = services;
    }

    @Override
    public String toString() {
        return "Branch{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", code='" + code + '\'' +
                ", email='" + email + '\'' +
                ", cellphone='" + cellphone + '\'' +
                ", address=" + address.toString() +
                '}';
    }
}
