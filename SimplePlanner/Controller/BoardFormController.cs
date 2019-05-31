﻿using SimplePlanner.Model;
using SimplePlanner.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePlanner.Controller
{
    public class BoardFormController
    {
        BoardForm boardForm;
        WorkForm workForm;

        public BoardData BoardData;
        public WorkData CurrentWork;
        public Label CurrentLabel;

        public bool IsClicked;
        public int WorkIndex;

        public BoardFormController(BoardForm _boardForm, WorkForm _workForm)
        {
            boardForm = _boardForm;
            workForm = _workForm;
            BoardData = new BoardData("New Board");
            CurrentWork = new WorkData();
            IsClicked = false;
        }

        public void OpenWorkForm()
        {
            if(IsClicked)
            {
                int tabIndex = boardForm.TabControl.SelectedIndex;
                TabData tabData = BoardData.Tabs.ElementAt(tabIndex);

                foreach (var item in tabData.Works)
                {
                    if (item.MyIndex == WorkIndex)
                    {
                        CurrentWork.WorkName = item.WorkName;
                        CurrentWork.WorkContent = item.WorkContent;
                        break;
                    }
                }

                workForm.WorkName = CurrentWork.WorkName;
                workForm.WorkContent = CurrentWork.WorkContent;
            }

            workForm.ShowDialog();
        }

        public void AddTab()
        {
            BoardData.Tabs.Add(new TabData("New Tab"));
            BoardData.TabUpdate(boardForm);
        }

        public void MoveCreateWorkBtn()
        {
            boardForm.TabControl.SelectedTab.Controls.Add(boardForm.CreateWorkBtn);
        }

        public void CreateWork()
        {
            int tabIndex = boardForm.TabControl.SelectedIndex;
            TabData tabData = BoardData.Tabs.ElementAt(tabIndex);
            tabData.Works.Add(new WorkData(CurrentWork.WorkName, CurrentWork.WorkContent));
            tabData.AddWork(boardForm);
        }

        public void UpdateWork()
        {
            int tabIndex = boardForm.TabControl.SelectedIndex;
            TabData tabData = BoardData.Tabs.ElementAt(tabIndex);

            foreach (var item in tabData.Works)
            {
                if(item.MyIndex == WorkIndex)
                {
                    item.WorkName = CurrentWork.WorkName;
                    item.WorkContent = CurrentWork.WorkContent;
                    item.Update(boardForm);
                    break;
                }
            }
        }
    }
}