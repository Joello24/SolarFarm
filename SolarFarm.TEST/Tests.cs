using NUnit.Framework;
using SolarFarm.DAL;
using SolarFarm.BLL;
using SolarFarm.Core.DTO;
using SolarFarm.UI;
using System;
using System.IO;

namespace SolarFarm.TEST
{
    [TestFixture]
    public class Add
    {
        Panel validPanel = new Panel();
        Panel duplicatePanel = new Panel();
        Panel panelInvalidColumn = new Panel();
        Panel panelInvalidRow = new Panel();
        Panel panelWithOutOfBoundsYear = new Panel();

        public PanelService service;
        [SetUp]
        public void Setup()
        {
            PanelRepo panelRepository = new();
            panelRepository.PATH = Directory.GetCurrentDirectory() + @"Panels.csv";
            service = new PanelService(panelRepository);

            validPanel.Section = "Main";
            validPanel.Row = 1;
            validPanel.Column = 1;
            validPanel.Material = Material.monocrystallineSilicon;
            validPanel.Year = 2020;
            validPanel.isTracking = true;

            duplicatePanel.Section = "Main";
            duplicatePanel.Row = 1;
            duplicatePanel.Column = 1;
            duplicatePanel.Material = Material.monocrystallineSilicon;
            duplicatePanel.Year = 2020;
            duplicatePanel.isTracking = true;

            panelInvalidColumn.Section = "Main";
            panelInvalidColumn.Row = 1;
            panelInvalidColumn.Column = 251;
            panelInvalidColumn.Material = (Material)2;
            panelInvalidColumn.Year = 2023;
            panelInvalidColumn.isTracking = true;

            panelInvalidRow.Section = "Main";
            panelInvalidRow.Row = -11;
            panelInvalidRow.Column = 1;
            panelInvalidRow.Material = (Material)2;
            panelInvalidRow.Year = 2023;
            panelInvalidRow.isTracking = true;

            panelWithOutOfBoundsYear.Section = "Main";
            panelWithOutOfBoundsYear.Row = 1;
            panelWithOutOfBoundsYear.Column = 1;
            panelWithOutOfBoundsYear.Material = Material.monocrystallineSilicon;
            panelWithOutOfBoundsYear.Year = 2023;
            panelWithOutOfBoundsYear.isTracking = true;
        }
        [Test]
        public void TestAddValidPanel()
        {

            Result<Panel> result = new Result<Panel>();

            result = service.Add(validPanel);
            Assert.IsTrue(result.Success, result.Message);
        }
        [Test]
        public void TestAddDuplicatePanel()
        {

            Result<Panel> result = new Result<Panel>();

            service.Add(validPanel);
            result = service.Add(duplicatePanel);
            Assert.IsFalse(result.Success, result.Message);
        }
        [Test]
        public void TestAddBadColumn()
        {
            Result<Panel> result = new Result<Panel>();

            result = service.Add(panelInvalidColumn);
            Assert.IsFalse(result.Success, result.Message);
        }
        [Test]
        public void TestAddBadRow()
        {
            Result<Panel> result = new Result<Panel>();

            result = service.Add(panelInvalidRow);
            Assert.IsFalse(result.Success, result.Message);
        }
        [Test]
        public void TestAddBadYear()
        {

            Result<Panel> result = new Result<Panel>();

            result = service.Add(panelWithOutOfBoundsYear);
            Assert.IsFalse(result.Success, result.Message);
        }
    }
    [TestFixture]
    public class Update
    {
        Panel validPanel = new Panel();
        Panel validPanelUpdater = new Panel();
        public PanelService service;
        [SetUp]
        public void Setup()
        {
            PanelRepo panelRepository = new();
            panelRepository.PATH = Directory.GetCurrentDirectory() + @"Panels.csv";
            service = new PanelService(panelRepository);

            validPanel.Section = "Main";
            validPanel.Row = 1;
            validPanel.Column = 1;
            validPanel.Material = Material.monocrystallineSilicon;
            validPanel.Year = 2020;
            validPanel.isTracking = true;

            validPanelUpdater.Section = "Main";
            validPanelUpdater.Row = 1;
            validPanelUpdater.Column = 1;
            validPanelUpdater.Material = Material.monocrystallineSilicon;
            validPanelUpdater.Year = 2020;
            validPanelUpdater.isTracking = true;
        }
        [Test]
        public void TestUpdateRow()
        {
            Result<Panel> result = new Result<Panel>();
            service.Add(validPanel);
            validPanelUpdater.Row = 2;
            result = service.Update(validPanelUpdater, validPanelUpdater);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Row, 2);
        }
        [Test]
        public void TestUpdateYear()
        {
            Result<Panel> result = new Result<Panel>();
            service.Add(validPanel);
            validPanelUpdater.Year = 2;
            result = service.Update(validPanelUpdater, validPanelUpdater);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Year, 2);
        }
        [Test]
        public void TestUpdateColumn()
        {
            Result<Panel> result = new Result<Panel>();
            service.Add(validPanel);
            validPanelUpdater.Column = 2;
            result = service.Update(validPanelUpdater, validPanelUpdater);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Column, 2);
        }
        [Test]
        public void TestUpdateSection()
        {
            Result<Panel> result = new Result<Panel>();
            service.Add(validPanel);
            validPanelUpdater.Section= "Not Main";
            result = service.Update(validPanelUpdater, validPanelUpdater);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Section, "Not Main");
        }
    }
    [TestFixture]
    public class Remove
    {
        Panel validPanel = new Panel();
        Panel InvalidPanel = new Panel();
        public PanelService service;
        [SetUp]
        public void Setup()
        {
            PanelRepo panelRepository = new();
            panelRepository.PATH = Directory.GetCurrentDirectory() + @"Panels.csv";
            service = new PanelService(panelRepository);

            validPanel.Section = "Main";
            validPanel.Row = 1;
            validPanel.Column = 1;
            validPanel.Material = Material.monocrystallineSilicon;
            validPanel.Year = 2020;
            validPanel.isTracking = true;

            InvalidPanel.Section = "Main";
            InvalidPanel.Row = 50;
            InvalidPanel.Column = 1;
            InvalidPanel.Material = Material.monocrystallineSilicon;
            InvalidPanel.Year = 2020;
            InvalidPanel.isTracking = true;
        }
        [Test]
        public void TestValidRemove()
        {
            Result<Panel> result = new Result<Panel>();
            service.Add(validPanel);
            result = service.Remove(validPanel.Section, validPanel.Row, validPanel.Column);

            Assert.IsTrue(result.Success);
        }
        [Test]
        public void TestInvalidRemove()
        {
            Result<Panel> result = new Result<Panel>();
            service.Add(validPanel);
            result = service.Remove(InvalidPanel.Section, InvalidPanel.Row, InvalidPanel.Column);

            Assert.IsFalse(result.Success);
            
        }
    }
}