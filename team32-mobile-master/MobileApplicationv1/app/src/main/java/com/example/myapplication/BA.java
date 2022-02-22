package com.example.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.SearchView;
import android.widget.Toast;

import java.util.ArrayList;

public class BA extends AppCompatActivity {
    ListView listView;
    ArrayList<String> list;
    ArrayAdapter adapter;
    String[] version = {"Campus Square","Cresta","Newtown","Sandton ","Ellof","Jourbet","The Glen","NorthCliff","Tsakane "
    };
    SearchView searchView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_b2);

        listView = findViewById(R.id.lv1);
        searchView = findViewById(R.id.searchView);

        list = new ArrayList<>();

        for (int i = 0;i<version.length;i++){

            list.add(version[i]);

        }

        adapter = new ArrayAdapter(BA.this,android.R.layout.simple_list_item_1, version);
        listView.setAdapter(adapter);

        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {

                Toast.makeText(BA.this, "Selected -> " + version[i], Toast.LENGTH_SHORT).show();
            }
        });

        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String s) {

                return false;
            }

            @Override
            public boolean onQueryTextChange(String s) {
                if(list.contains(s)){
                    adapter.getFilter().filter(s);
                }
                return true;
            }
        });
    }
}
