    using System.Text;
public class Comment : IComparable<Comment>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public Childs Childs { get; } = new Childs();
    public string Text { get; set; }

    public int CompareTo(Comment other)
    {
        return Id.CompareTo(other.Id);
    }
}

public class CommentComparer : IComparer<Comment>
{
    public int Compare(Comment x, Comment y)
    {
        return x.Id.CompareTo(y.Id);
    }
}

public class Childs
{
    public List<Comment> Children { get; } = new List<Comment>();

    public Childs()
    {
        Children.Sort(new CommentComparer());
    }

    public void AddComment(Comment comment)
    {
        Children.Add(comment);
        Children.Sort(new CommentComparer());
    }
}


class Program
{
    static string _output(Childs comments, string line = "", string upperline = "",string childline="")
    {
        StringBuilder result = new StringBuilder();
        int i = 0;
        while (i < comments.Children.Count())
        {
            result.AppendLine(line + upperline);
            result.AppendLine(line + upperline + childline + comments.Children[i].Text);
            
            if (comments.Children[i].Childs.Children.Count() != 0)
            {
                if (i != comments.Children.Count() - 1)
                {
                    result.Append(_output(comments.Children[i].Childs, line + "|  ", "|", "--"));
                }
                else
                {
                    result.Append(_output(comments.Children[i].Childs, line + "   ", "|", "--"));
                }
            }
            i++;
        }
        return result.ToString();
    }

    static string output(Childs comments, string line = "", string upperline = "", string childline = "")
    {
        StringBuilder result = new StringBuilder();
        int i = 0;
        while (i < comments.Children.Count())
        {
            result.AppendLine(comments.Children[i].Text);
            if (comments.Children[i].Childs.Children.Count() != 0)
            {
                if (i != comments.Children.Count() - 2)
                {
                    result.AppendLine(_output(comments.Children[i].Childs, line, "|", "--"));
                }
                else
                {
                    result.AppendLine(_output(comments.Children[i].Childs, line, "|", "--"));
                }
            }
            else
            {
                result.AppendLine();
            }
            i++;
        }
        return result.ToString();
    }

    static bool v3(Childs comments, Childs nsc, Comment comment, int id = -1)
    {
        if (comment.ParentId == -1)
        {
            comments.AddComment(comment);
            
        }
        else
        {
            nsc.AddComment(comment);
        }
        
        return v2(comments, nsc);
    }
    static bool v2(Childs comments, Childs nsc)
    {
        int i = 0;
        while(i < comments.Children.Count())
        {
            int j = 0;
            while (j < nsc.Children.Count())
            {
                if (comments.Children[i].Id == nsc.Children[j].ParentId)
                {
                    comments.Children[i].Childs.AddComment(nsc.Children[j]);
                    nsc.Children.Remove(nsc.Children[j]);
                    continue;
                }

                j++;
            }
            if(nsc.Children.Count() !=0) 
            {
                v2(comments.Children[i].Childs,nsc);
            }
            i++;
        }
        return true;
    }

    static void Main()
    {
        int n = Convert.ToInt32(Console.ReadLine());
        StringBuilder result = new StringBuilder();
        for (int o = 0; o < n; o++)
        {
            int t = Convert.ToInt32(Console.ReadLine());
            Childs comments = new Childs();
            Childs nsc = new Childs();

            for (int i = 0; i < t; i++)
            {
                string line = Console.ReadLine();
                string[] lines = line.Split(" ");
                Comment comment = new Comment()
                {
                    Id = Convert.ToInt32(lines[0]),
                    ParentId = Convert.ToInt32(lines[1]),
                    Text = string.Join(" ", lines, 2, lines.Length - 2),
                };

                v3(comments, nsc, comment);
            }
            result.Append(output(comments));
        }

        Console.Write(result.ToString());
    }
}
        