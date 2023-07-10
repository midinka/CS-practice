using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelloWorld;

public class Student: IEquatable<Student>
{
    private string _surname;
    private string _name;
    private string _middlename;

    private string _group;
    private int _course;
    public enum practice_course_mode : int
    {
        C,
        Go,
        Yandex
    }

    private practice_course_mode _practice_course;

    public Student(string surname,
        string name,
        string middlename,
        string group,
        practice_course_mode practice_course
    )
    {
        _surname = surname ?? throw new ArgumentNullException(nameof(surname));
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _middlename = middlename ?? throw new ArgumentNullException(nameof(middlename));
        _group = group ?? throw new ArgumentNullException(nameof(group));
        _practice_course = practice_course;


        int count = 0;
        foreach (char i in group)
        {
            if (count == 1)
            {
                _course = i - '0';
                break;
            }
            if (i == '-') count++;

        }
    }

    public string Surname
    {
        get { return _surname; }
    }

    public string Name
    { 
        get { return _name; } 
    }

    public string Middlename
    {
        get { return _middlename; }
    }

    public string Group
    {
        get => _group;
    }

    public practice_course_mode Practice_Course
    {
        get => _practice_course;
    }

    public int Course
    {
        get => _course;
    }


    public override string ToString()
    {
        return $"[ Surname: {_surname}, Name: {_name}, Middle Name: {_middlename}, Group: {_group}, Practice Course: {_practice_course} ]";
    }

    public override int GetHashCode()
    {
        return ((((_name.GetHashCode()*2 + _surname.GetHashCode())*3 + _middlename.GetHashCode())*5 + _practice_course.GetHashCode())*7 + _group.GetHashCode())*11;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (obj is Student std)
        {
            return Equals(std);
        }

        return false;
    }

    public bool Equals(Student std)
    {
        if (std == null) return false;

        return _name.Equals(std._name) && _surname.Equals(std._surname) && _middlename.Equals(std._middlename) && _group.Equals(std._group) && _practice_course == std._practice_course;
    }

}
