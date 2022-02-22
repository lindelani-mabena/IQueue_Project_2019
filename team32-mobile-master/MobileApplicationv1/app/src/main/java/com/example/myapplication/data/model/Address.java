package com.example.myapplication.data.model;

import org.json.JSONException;
import org.json.JSONObject;

public class Address {
    private int id;
    private String street;
    private String town;
    private String city;
    private String province;
    private String latitude;
    private String longitude;
    private String zip;

    public Address(int Id, String Address1, String Address2, String City, String Province,
                   String Zip) {
        id = Id;
        street = Address1;
        town = Address2;
        city = City;
        province = Province;
        zip = Zip;
    }

    public Address(JSONObject jsonObject) {
        try {
            id = Integer.parseInt(jsonObject.getString("Id"));
            street = jsonObject.getString("Address1");
            town = jsonObject.getString("Address2");
            city = jsonObject.getString("City");
            province = jsonObject.getString("Province");
            zip = jsonObject.getString("Zip");
            latitude = jsonObject.getString("Latitude");
            longitude = jsonObject.getString("Longitude");
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

    public String getStreet() {
        return street;
    }

    public void setStreet(String street) {
        this.street = street;
    }

    public String getTown() {
        return town;
    }

    public void setTown(String town) {
        this.town = town;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getProvince() {
        return province;
    }

    public void setProvince(String province) {
        this.province = province;
    }

    public String getZip() {
        return zip;
    }

    public void setZip(String zip) {
        this.zip = zip;
    }

    public String getLatitude() {
        return latitude;
    }

    public String getLongitude() {
        return longitude;
    }

    public void setLatitude(String latitude) {
        this.latitude = latitude;
    }

    public void setLongitude(String longitude) {
        this.longitude = longitude;
    }

    @Override
    public String toString() {
        return "Address{" +
                "id=" + id +
                ", street='" + street + '\'' +
                ", town='" + town + '\'' +
                ", city='" + city + '\'' +
                ", province='" + province + '\'' +
                ", zip='" + zip + '\'' +
                '}';
    }
}
