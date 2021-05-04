using System;
using System.Collections.Generic;

namespace Assignment
{
    class MemberCollection : iMemberCollection
    {
        class MemberNode : IComparable<MemberNode>
        {
            public iMember member;
            public MemberNode prev = null;
            public MemberNode next = null;
            public MemberNode(iMember member)
            {
                this.member = member;
            }

            public int CompareTo(MemberNode other)
            {
                return member.ContactNumber.CompareTo(other.member.ContactNumber);
            }

            public static bool operator <(MemberNode a, MemberNode b)
            {
                return a.CompareTo(b) < 0;
            }
            public static bool operator >(MemberNode a, MemberNode b)
            {
                return a.CompareTo(b) > 0;
            }
        }
        MemberNode root;

        public MemberCollection()
        {
            root = null;
        }

        public int Number { get => toArray().Length; }

        // Parent cannot be null
        static void insertNode(MemberNode newNode, ref MemberNode parent)
        {
            ref MemberNode currHead = ref parent;
            while (true)
            {
                // Set the parent node for this iteration
                parent = ref currHead;
                if (newNode < currHead)
                {
                    // Branch out backwards
                    currHead = ref currHead.prev;
                    // If the node is free, insert into it
                    if (currHead == null)
                    {
                        parent.prev = newNode;
                        return;
                    }
                }
                else
                {
                    // Branch out forwards
                    currHead = ref currHead.next;
                    // If the node is free, insert into it
                    if (currHead == null)
                    {
                        parent.next = newNode;
                        return;
                    }
                }
            }
        }
        public void add(iMember member)
        {
            MemberNode newNode = new MemberNode(member);
            if (root == null) root = newNode;
            insertNode(newNode, ref root);
        }
        public void delete(iMember member)
        {
            // Deletion of root node
            if (root.member == member)
            {
                // If no node is greater than root
                if (root.next == null)
                {
                    root = root.prev;
                }
                // root.next definitely exists - at least one child
                else if (root.prev == null) root = root.next;
                // root has 2 children
                else deleteNonMax(ref root);
                return;
            }
            // Deletion of some descendant
            removeNode(new MemberNode(member), ref root);
        }

        // Given a non-max node, "delete" it
        static void deleteNonMax(ref MemberNode target)
        {
            if (target.next == null) throw new ArgumentException("Given node is a max node");
            MemberNode currHead = target.next;
            ref MemberNode parent = ref target;
            // Drill down until there's no descendant node that is further to the left
            while (currHead.next != null)
            {
                parent = currHead;
                currHead = currHead.next;
            }
            // Assign the data from the rightmost node to the target
            target.member = currHead.member;
            // Remove the rightmost descendant node
            parent.next = null;
        }

        // Remove a node, given a parent node
        static void removeNode(MemberNode target, ref MemberNode parent)
        {
            while (true)
            {
                if (parent == null) throw new IndexOutOfRangeException("The member is not part of the collection");
                if (target < parent)
                {
                    // Remove the parent's prev
                    if (parent.prev.member == target.member)
                    {
                        MemberNode nodeToRemove = parent.prev;
                        if (nodeToRemove.next == null) parent.prev = nodeToRemove.prev;
                        else if (nodeToRemove.prev == null) parent.prev = nodeToRemove.next;
                        else deleteNonMax(ref parent.prev);
                        return;
                    }
                    // Drill down into the prev
                    parent = ref parent.prev;
                }
                else
                {
                    // Remove the parent's next
                    if (parent.next.member == target.member)
                    {
                        MemberNode nodeToRemove = parent.next;
                        if (nodeToRemove.next == null) parent.next = nodeToRemove.prev;
                        else if (nodeToRemove.prev == null) parent.next = nodeToRemove.next;
                        else deleteNonMax(ref parent.next);
                        return;
                    }
                    // Drill down into the next
                    parent = ref parent.next;
                }
            }
        }

        public bool search(iMember member)
        {
            try {
                get(member);
                return true;
            }
            catch (IndexOutOfRangeException) {
                return false;
            }
        }

        // Get a member from the collection
        public ref iMember get(iMember member)
        {
            MemberNode target = new MemberNode(member);
            MemberNode head = root;
            while (head != null)
            {
                // If the head is the right node
                if (target.member == head.member) return ref head.member;
                // Too far left
                if (target < head) head = head.prev;
                // Too far right
                else head = head.next;
            }
            // There's nothing at that value's position
            throw new IndexOutOfRangeException("Member not found in collection");
        }

        public iMember[] toArray()
        {
            // Initialise an array of members
            Queue<Member> members = new Queue<Member>();
            int index = 0;
            // Add a member to the queue
            Action<Member> addMember = (Member member) =>
            {
                members.Enqueue(member);
                index++;
            };
            Stack<MemberNode> memberStack = new Stack<MemberNode>();
            MemberNode curr = root;
            // Traverse
            while (curr != null || memberStack.Count > 0)
            {
                // Reach the leftmost node
                while (curr != null)
                {
                    memberStack.Push(curr);
                    curr = curr.prev;
                }
                // curr == null
                curr = memberStack.Pop();
                addMember((Member)curr.member);
                // Go to the next subtree
                curr = curr.next;
            }
            // Convert to an array
            return members.ToArray();
        }
    }
}