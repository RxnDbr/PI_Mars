﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Mission
{

    //attributes, properties and accessors

	private int howmanydays;
    public int HomanyDays { get {return howmanydays;}}

    private bool beginMission;
    public bool BeginMission { get; }

	private DateTime beginningDateEarth;
    public DateTime BeginningDateEarth
	{
        get { if (beginMission) return beginningDateEarth; return DateTime.MinValue; }
		set 
        {
            DateTime now = DateTime.Now;
            if (beginningDateEarth<now) {beginningDateEarth = value;};
        }
	}

	private List<String> l_genericActivity ;
    public List<String> L_genericActivity { get { return l_genericActivity; } } //get general activity from XML

    private List<Day> l_day;
    public List<Day> L_day { get { return L_day; } }

    private List<Astronaut> l_astronaut;
    public List<Astronaut> L_astronaut { get { return l_astronaut; } }

    private List<Place> l_place;
    public List<Place> L_place { get { return l_place; } }


    //constructor

    public Mission(int _howmanydays, List<Astronaut> _l_astronaut)
	{
        howmanydays = _howmanydays;
        beginMission = false;
        l_astronaut = _l_astronaut;
        l_day = new List<Day>();
        for (int i=0 ; i <= howmanydays; i++) {Future Day = new Future(i);}
        //generate l_activity from XML
        l_place = new List<Place>();
	}

    //methodes

    public void add_astronaut(Astronaut astro)
    {
        DateTime now = DateTime.Now;
        if (beginningDateEarth < now) { l_astronaut.Add(astro); }
    }

    public void rm_astronaut(Astronaut astro) 
    {
        DateTime now = DateTime.Now; 
        if (l_astronaut.Contains(astro) && beginningDateEarth < now) l_astronaut.Remove(astro);
    }

	public Day getDay(int numDay) {if (numDay >=0 && numDay<=howmanydays) {return l_day[numDay];} return null;} 

	public List<Place> getPlaces(Day start, Day end)
	{
        List<Place> lp = new List<Place>();
        for (int i = start.Number; i <= end.Number; i++)
        {
            if (l_day[i].Outside)
            {
                foreach (Outside out_act in l_day[i].l_activity) { lp.Add(out_act.Place); }
            }
        }
        return lp;
	}

    //si ne rentre aucune valeur, alors prend du début à la fin
    //If getPlace is called without any parameters , process from the beginning of the mission untill the end
    public List<Place> getPlaces() { return getPlaces(l_day[0], l_day[howmanydays - 1]); }

	public void startMission(DateTime CurrentEarthDate)
	{
        //beginningDateEarth = DateTime.Now;
        if (beginningDateEarth == null) { beginningDateEarth = CurrentEarthDate; }
        int gap = CurrentEarthDate.Day - beginningDateEarth.Day;
        beginMission = true;
        for (int i = 0; i <= gap; i++) { changeDayStatus(l_day[i], "future", "present"); }
	}

    public void changeDayStatus(Day d, string from, string to)
    {
        Day tmp = d;
        switch (to)
        {
            case "past":
                l_day[d.Number] = new Past(d.Number);
                break;
            case "present":
                l_day[d.Number] = new Present(d.Number);
                break;
            default: //case "future":
                l_day[d.Number] = new Future(d.Number);
                break;
        }

        l_day[d.Number].Report = d.Report;
        l_day[d.Number].Outside = d.Outside;


        //(d.Number, d.Report, ;

    }

    public void addActivity (string act)
    {
        l_genericActivity.Add(act);
        //le mettre dans ke xml

    }

}

