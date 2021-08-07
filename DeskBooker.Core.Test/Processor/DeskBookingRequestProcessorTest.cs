using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using Moq;
using System;
using Xunit;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessorTest
    {
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private readonly DeskBookingRequestProcessor _processor;
        private readonly DeskBookingRequest _request;

        public DeskBookingRequestProcessorTest()
        {

            _deskBookingRepositoryMock = new Mock<IDeskBookingRepository>();
            _processor = new DeskBookingRequestProcessor(_deskBookingRepositoryMock.Object);

            _request = new DeskBookingRequest
            {
                FirstName = "Mateusz",
                LastName = "Glowa",
                Email = "glowa.mateusz@gmail.com",
                Date = new DateTime(2020, 1, 28)
            };
        }

        [Fact]
        public void ShouldReturnDeskBookingResultWithRequestValues()
        {
            //Act
            DeskBookingResult result = _processor.BookDesk(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(_request.FirstName, result.FirstName);
            Assert.Equal(_request.LastName, result.LastName);
            Assert.Equal(_request.Email, result.Email);
            Assert.Equal(_request.Date, result.Date);

        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {

            var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookDesk(null));

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ShouldSaveDeskBooking()
        {
            DeskBooking saveDeskBooking = null;
            _deskBookingRepositoryMock.Setup(x => x.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(deskBooking =>
                {
                    saveDeskBooking = deskBooking;
                });

            _processor.BookDesk(_request);

            //TO COMPLETE - TODO chapter 3 - test: save a desk booking
            //https://app.pluralsight.com/course-player?clipId=71946b80-9a7f-430e-82d0-7095ef5c09ab
        }
    }
}
