package com.example.myapplication;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.MenuItem;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBarDrawerToggle;
import androidx.appcompat.app.AppCompatActivity;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.fragment.app.FragmentActivity;

import com.androidnetworking.AndroidNetworking;
import com.androidnetworking.common.Priority;
import com.androidnetworking.error.ANError;
import com.androidnetworking.interfaces.JSONArrayRequestListener;
import com.example.myapplication.R;
import com.example.myapplication.ServiceList;
import com.example.myapplication.data.Network;
import com.example.myapplication.data.model.Branch;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.material.navigation.NavigationView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class BranchesMapsActivity extends AppCompatActivity implements OnMapReadyCallback,
        GoogleMap.OnInfoWindowClickListener {

    private GoogleMap mMap;
    private List<Branch> branches;
    private Handler handler = new Handler();
    private Runnable runnable;
    private int apiDelayed = 5 * 1000;
    HashMap<Marker, Branch> hashMap = new HashMap<Marker, Branch>();

    private DrawerLayout dl;
    private ActionBarDrawerToggle t;
    private NavigationView nv;

    Members member;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_branches_maps);
        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        member = new Members();

        dl = (DrawerLayout)findViewById(R.id.mapsact);
        t = new ActionBarDrawerToggle(this, dl,R.string.app_name,R.string.app_name);

        dl.addDrawerListener(t);
        t.syncState();

        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        nv = (NavigationView)findViewById(R.id.nv);
        nv.setNavigationItemSelectedListener(
                new NavigationView.OnNavigationItemSelectedListener() {
                    @Override
                    public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                        int id = item.getItemId();
                        switch(id)
                        {
                            case R.id.home:
                                Toast.makeText(BranchesMapsActivity.this, "Update Profile",Toast.LENGTH_SHORT).show();
                                Intent intent6 = new Intent(BranchesMapsActivity.this, HomeP.class);

                                startActivity(intent6);break;
                            case R.id.update:
                                Toast.makeText(BranchesMapsActivity.this, "Update Profile",Toast.LENGTH_SHORT).show();
                                Intent intent2 = new Intent(BranchesMapsActivity.this, Update_Profile.class);

                                startActivity(intent2);break;
                            case R.id.login:
                                Toast.makeText(BranchesMapsActivity.this, "Login",Toast.LENGTH_SHORT).show();
                                Intent intent = new Intent(BranchesMapsActivity.this, Login.class);

                                startActivity(intent);break;
                            case R.id.register:
                                Toast.makeText(BranchesMapsActivity.this, "Register",Toast.LENGTH_SHORT).show();
                                Intent intent4 = new Intent(BranchesMapsActivity.this, Register.class);

                                startActivity(intent4);break;
                            default:
                                return true;
                        }
                        return true;
                    }
                });
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if(t.onOptionsItemSelected(item))
            return true;
        return super.onOptionsItemSelected(item);
    }

    @Override
    protected void onResume() {
        super.onResume();

        handler.postDelayed(runnable = new Runnable() {
            public void run() {
                //do your function.
                //mMap.setMyLocationEnabled(true);
                branches = new ArrayList<>();
                String url = Network.BASEADDRESS + "api/branches";

                AndroidNetworking.get(url)
                        .setPriority(Priority.LOW)
                        .build()
                        .getAsJSONArray(new JSONArrayRequestListener() {
                            @Override
                            public void onResponse(JSONArray response) {
                                // do anything with response
                                for(int i = 0; i < response.length(); i++) {
                                    try {
                                        JSONObject jsonObject = response.getJSONObject(i);
                                        Branch branch = new Branch(jsonObject);

                                        LatLng point = new LatLng(Double.parseDouble(branch.getAddress().getLatitude()),
                                                Double.parseDouble(branch.getAddress().getLongitude()));

                                        member.setBranchName(branch.getName());
                                        member.setBranchID(branch.getId());

                                        Marker marker = mMap.addMarker(new MarkerOptions()
                                                .position(point)
                                                .title(branch.getName())
                                                .snippet("Services: " + branch.getServices().size())
                                        );
                                        hashMap.put(marker, branch);
                                        branches.add(branch);
                                    } catch (JSONException e) {
                                        e.printStackTrace();
                                    }
                                }
                            }
                            @Override
                            public void onError(ANError error) {
                                // handle error
                                Log.v("MAPS", error.getMessage());
                            }
                        });

                handler.postDelayed(runnable, apiDelayed);

            }

        }, apiDelayed); // so basically after your getHeroes(), from next time it will be 5 sec repeated
    }

    @Override
    protected void onPause() {
        super.onPause();

        handler.removeCallbacks(runnable);
    }

    /**
     * Manipulates the map once available.
     * This callback is triggered when the map is ready to be used.
     * This is where we can add markers or lines, add listeners or move the camera. In this case,
     * we just add a marker near Sydney, Australia.
     * If Google Play services is not installed on the device, the user will be prompted to install
     * it inside the SupportMapFragment. This method will only be triggered once the user has
     * installed Google Play services and returned to the app.
     */
    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;
        //mMap.setMyLocationEnabled(true);

        branches = new ArrayList<>();
        String url = Network.BASEADDRESS + "api/branches";

        AndroidNetworking.get(url)
                .setPriority(Priority.LOW)
                .build()
                .getAsJSONArray(new JSONArrayRequestListener() {
                    @Override
                    public void onResponse(JSONArray response) {
                        // do anything with response
                        for(int i = 0; i < response.length(); i++) {
                            try {
                                JSONObject jsonObject = response.getJSONObject(i);
                                Branch branch = new Branch(jsonObject);

                                LatLng point = new LatLng(Double.parseDouble(branch.getAddress().getLatitude()),
                                        Double.parseDouble(branch.getAddress().getLongitude()));

                                Marker marker = mMap.addMarker(new MarkerOptions()
                                        .position(point)
                                        .title(branch.getName())
                                        .snippet("Services: " + branch.getServices().size())
                                );
                                hashMap.put(marker, branch);
                                branches.add(branch);
                            } catch (JSONException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                    @Override
                    public void onError(ANError error) {
                        // handle error
                        Log.v("MAPS", error.getMessage());
                    }
                });
        mMap.setOnInfoWindowClickListener(this);
    }

    @Override
    public void onInfoWindowClick(Marker marker) {
        Branch branch = hashMap.get(marker);
        if (branch != null) {
            if (branch.getServices().size() > 0) {
                Intent intent = new Intent(getApplicationContext(), ServiceList.class);
                intent.putExtra("ID", branch.getId() + "");
                member.setBranchName(branch.getName());
                startActivity(intent);
            } else {
                Toast.makeText(getApplicationContext(), branch.getName() + " has no services",
                        Toast.LENGTH_LONG);
            }
        }
    }
}

