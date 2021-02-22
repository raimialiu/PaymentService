using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessPayment.Migrations
{
    public partial class addedPaymentIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_paymentState_statePaymentStateId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_statePaymentStateId",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "statePaymentStateId",
                table: "payments");

            migrationBuilder.AddColumn<int>(
                name: "paymentId",
                table: "paymentState",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_paymentState_paymentId",
                table: "paymentState",
                column: "paymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_paymentState_payments_paymentId",
                table: "paymentState",
                column: "paymentId",
                principalTable: "payments",
                principalColumn: "PaymentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentState_payments_paymentId",
                table: "paymentState");

            migrationBuilder.DropIndex(
                name: "IX_paymentState_paymentId",
                table: "paymentState");

            migrationBuilder.DropColumn(
                name: "paymentId",
                table: "paymentState");

            migrationBuilder.AddColumn<int>(
                name: "statePaymentStateId",
                table: "payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_statePaymentStateId",
                table: "payments",
                column: "statePaymentStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_paymentState_statePaymentStateId",
                table: "payments",
                column: "statePaymentStateId",
                principalTable: "paymentState",
                principalColumn: "PaymentStateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
